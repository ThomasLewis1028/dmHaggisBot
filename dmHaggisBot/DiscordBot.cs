using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using SWNUniverseGenerator;

namespace dmHaggisBot
{
    internal class DiscordBot
    {
        private static Universe _universe;
        private static Creation _creation;

        //Properties file
        private static readonly JObject Prop =
            JObject.Parse(
                File.ReadAllText(@"properties.json"));

        private static readonly string Token = (string) Prop.GetValue("token");
        private readonly Regex _charChreate = new Regex("^(charCreate|createChar|cc)($|.*)", RegexOptions.IgnoreCase);
        private readonly Regex _starCreate = new Regex("^(starCreate|createStar|sc|cs)($|.*)", RegexOptions.IgnoreCase);

        private readonly Regex _planCreate =
            new Regex("^(planetCreate|createPlanet|pc|cp)($|.*)", RegexOptions.IgnoreCase);

        private readonly Regex _univChreate =
            new Regex("^(univCreate|createUniv|uc|cu)($|.*)", RegexOptions.IgnoreCase);

        private readonly Regex _univLoad = new Regex("^(univLoad|loadUniv|ul|lu)($|.*)", RegexOptions.IgnoreCase);
        private readonly Regex _dataSearch = new Regex("^(dataSearch|searchData|ds|sd)($|.*)", RegexOptions.IgnoreCase);
        private DiscordSocketClient _client;
        private IConfiguration _config;
        private Creation creation;

        private static string universePath
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                if (!Directory.Exists(Path.GetDirectoryName(path) + "\\UniverseFiles\\"))
                    Directory.CreateDirectory(Path.GetDirectoryName(path) + "\\UniverseFiles\\");
                return Path.GetDirectoryName(path) + "\\UniverseFiles\\";
            }
        }

        public async Task MainAsync()
        {
            var _config = new DiscordSocketConfig {MessageCacheSize = 100};
            // _client.Log += message => Console.Out.WriteLine();
            _client = new DiscordSocketClient(_config);
            _client.MessageReceived += MessageReceived;
            _client.ReactionAdded += ReactionAdded;

            await _client.LoginAsync(TokenType.Bot, Token);
            await _client.StartAsync();
            await _client.SetGameAsync("No Universe Loaded");

            Console.Out.WriteLine("DiscordBot Connected");

            await Task.Delay(-1);
        }

        public async Task MessageReceived(SocketMessage sm)
        {
            if (sm.Author.IsBot)
                return;

            if (_charChreate.IsMatch(sm.Content))
            {
                if (_universe != null)
                    CreateChar(sm);
                else
                    await sm.Channel.SendMessageAsync("No universe file loaded");
            }

            if (_univChreate.IsMatch(sm.Content))
                CreateUniv(sm);

            if (_univLoad.IsMatch(sm.Content))
                LoadUniv(sm);

            if (_starCreate.IsMatch(sm.Content))
            {
                if (_universe != null)
                    CreateStar(sm);
                else
                    await sm.Channel.SendMessageAsync("No universe file loaded");
            }

            if (_planCreate.IsMatch(sm.Content))
            {
                if (_universe != null)
                    CreatePlanet(sm);
                else
                    await sm.Channel.SendMessageAsync("No universe file loaded");
            }

            if (_dataSearch.IsMatch(sm.Content))
            {
                if (_universe != null)
                    SearchData(sm);
                else
                    await sm.Channel.SendMessageAsync("No universe file loaded");
            }
        }

        public async Task ReactionAdded(Cacheable<IUserMessage, ulong> cacheable, ISocketMessageChannel sc,
            SocketReaction sr)
        {
            bool up = true;

            if (!_dataSearch.IsMatch(sr.Message.ToString()))
                return;

            if (ComputeSha256Hash(sr.Emote.Name) == "8f9628264c08fdeade2ff56f7ff8fb0d893fc8a6c01328b9aa05aab780f22016")
                up = true;

            else if (ComputeSha256Hash(sr.Emote.Name) ==
                     "b6abcc7620e24f7b17a6b4148deee61b1613c204bfbefe417309f9460ff007e9")
                up = false;

            var results = ParsePagination(sr.Message.ToString(), up);
            var searchDefaultSettings = results.Item1;
            var message = results.Item2;

            await SearchData(sr.Channel, message, searchDefaultSettings);

            sr.Channel.DeleteMessageAsync(sr.Message.Value, RequestOptions.Default);
        }

        public async Task CreateUniv(SocketMessage sm)
        {
            var g = ParseCommand("g", sm.Content);
            var n = ParseCommand("n", sm.Content);
            var o = ParseCommand("o", sm.Content);

            var univDef = new UniverseDefaultSettings();

            univDef.Name = n;
            univDef.Grid = string.IsNullOrEmpty(g)
                ? null
                : g.Split(" ").Length == 1
                    ? new Grid(Int32.Parse(g), Int32.Parse(g))
                    : new Grid(Int32.Parse(g.Split(" ")[0]), Int32.Parse(g.Split(" ")[1]));
            univDef.Overwrite = o;

            try
            {
                _creation = new Creation(universePath);
                _universe = _creation.CreateUniverse(univDef);
                _universe.Characters = new List<Character>();
                _universe.Stars = new List<Star>();
                _universe.Planets = new List<Planet>();

                await sm.Channel.SendMessageAsync("Created Universe with the name " + _universe.Name);
                SetGameStatus();
            }
            catch (IOException e)
            {
                await sm.Channel.SendMessageAsync(e.ToString());
            }
        }
        
        public async Task LoadUniv(SocketMessage sm)
        {
            var n = ParseCommand("n", sm.Content);

            try
            {
                _creation = new Creation(universePath);
                _universe = _creation.LoadUniverse(n);
                await sm.Channel.SendMessageAsync("Loaded Universe with the name " + _universe.Name);
                SetGameStatus();
            }
            catch (FileNotFoundException e)
            {
                await sm.Channel.SendMessageAsync(e.Message);
            }
        }

        public async Task CreateStar(SocketMessage sm)
        {
            var s = ParseCommand("s", sm.Content);

            var starDef = new StarDefaultSettings();

            starDef.StarCount = string.IsNullOrEmpty(s)
                ? 0
                : Int32.Parse(s);

            try
            {
                _universe = _creation.CreateStars(_universe, starDef);
                await sm.Channel.SendMessageAsync(s + " stars created in " + _universe.Name);
                SetGameStatus();
            }
            catch (FileNotFoundException e)
            {
                await sm.Channel.SendMessageAsync(e.Message);
            }
        }
        
        public async Task CreatePlanet(SocketMessage sm)
        {
            var r = ParseCommand("r", sm.Content);
            var pr = string.IsNullOrEmpty(r)
                ? new[] {0, 0}
                : r.Split(" ").Length == 1
                    ? new[] {Int32.Parse(r), Int32.Parse(r)}
                    : new[] {Int32.Parse(r.Split(" ")[0]), Int32.Parse(r.Split(" ")[1])};

            var planDef = new PlanetDefaultSettings();

            planDef.PlanetRange = pr;

            try
            {
                _universe = _creation.CreatePlanets(_universe, planDef);
                await sm.Channel.SendMessageAsync("Up to " + pr[1] + " planets created for each star" + _universe.Name);
                SetGameStatus();
            }
            catch (FileNotFoundException e)
            {
                await sm.Channel.SendMessageAsync(e.Message);
            }
        }
        
        public async Task CreateChar(SocketMessage sm)
        {
            var a = ParseCommand("a", sm.Content);
            var ar = string.IsNullOrEmpty(a)
                ? new[] {-1, -1}
                : a.Split(" ").Length == 1
                    ? new[] {Int32.Parse(a), Int32.Parse(a),}
                    : new[] {Int32.Parse(a.Split(" ")[0]), Int32.Parse(a.Split(" ")[1])};
            var c = ParseCommand("c", sm.Content);
            var g = ParseCommand("g", sm.Content);
            var f = ParseCommand("f", sm.Content);
            var l = ParseCommand("l", sm.Content);
            var h = ParseCommand("h", sm.Content);
            var hc = ParseCommand("hc", sm.Content);
            var ec = ParseCommand("ec", sm.Content);
            var t = ParseCommand("t", sm.Content);

            // await sm.Channel.SendMessageAsync(sb.ToString());
            var charDef = new CharacterDefaultSettings();

            charDef.Count = string.IsNullOrEmpty(c) ? 1 : int.Parse(c);
            charDef.First = f;
            charDef.Last = l;
            charDef.Age = ar;
            charDef.HairStyle = h;
            charDef.HairCol = hc;
            charDef.EyeCol = ec;
            charDef.Title = t;

            if (g == "0")
                charDef.Gender = Character.GenderEnum.Male;
            else if (g == "1")
                charDef.Gender = Character.GenderEnum.Female;
            else
                charDef.Gender = Character.GenderEnum.Undefined;

            try
            {
                _universe = _creation.CreateCharacter(_universe, charDef);

                await sm.Channel.SendMessageAsync(charDef.Count + " new character(s) created in " + _universe.Name);
                SetGameStatus();
            }
            catch (FileNotFoundException e)
            {
                await sm.Channel.SendMessageAsync(e.Message);
            }
        }
        
        public async Task SearchData(SocketMessage sm)
        {
            var id = ParseCommand("id", sm.Content);
            var n = ParseCommand("n", sm.Content);
            var c = ParseCommand("c", sm.Content);
            var t = ParseCommand("t", sm.Content);

            SearchDefaultSettings searchDef = new SearchDefaultSettings();

            searchDef.ID = id;
            searchDef.Name = n;
            searchDef.Count = string.IsNullOrEmpty(c)
                ? 0
                : Int32.Parse(c);
            searchDef.Tag = t;

            await SearchData(sm.Channel, sm.Content, searchDef);
        }

        private async Task SearchData(ISocketMessageChannel sc, string sm,
            SearchDefaultSettings searchDefaultSettings)
        {
            var results = _creation.SearchUniverse(_universe, searchDefaultSettings);
            var embeds = new List<Embed>();

            if (results.Result != null)
            {
                if (results.Result.GetType() == typeof(Character))
                    embeds.Add(GenerateEmbeds.CharacterEmbed(_universe, (Character) results.Result));
                else if (results.Result.GetType() == typeof(Planet))
                    embeds.Add(GenerateEmbeds.PlanetEmbed(_universe, (Planet) results.Result));
                else if (results.Result.GetType() == typeof(Star))
                    embeds.Add(GenerateEmbeds.StarEmbed(_universe, (Star) results.Result));

                await sc.SendMessageAsync(
                    sm + " - [" + results.CurrentCount + ", " + results.MaxCount + "]", false, embeds[0]);
            }
            else
            {
                await sc.SendMessageAsync("No results found");
            }
        }

        private string ParseCommand(string argName, string argVal)
        {
            var start = argVal.IndexOf(" -" + argName + " ") + 1;
            if (start == 0)
                return "";

            var end = argVal.IndexOf(" -", start);
            if (end == -1)
                end = argVal.Length - 1;

            var cmd = argVal.Substring(start + 2 + argName.Length, end - start - argName.Length - 1);

            return cmd.Trim();
        }
        
        public (SearchDefaultSettings, string) ParsePagination(String message, bool up)
        {
            var id = ParseCommand("id", message);
            var n = ParseCommand("n", message);
            var c = ParseCommand("c", message);
            var t = ParseCommand("t", message);

            Match match = Regex.Match(message, @"\s\-\s\[\d+\,\s\d+\]$");
            if (match.Success)
            {
                var start = match.Index;
                var cs = message.Substring(start);
                var cn = message.Substring(start).Replace(" - [", "").Replace("]", "").Split(", ")[0].Trim();
                c = (Int32.Parse(cn) + (up ? 1 : -1)).ToString();
                message = message.Replace(cs, "");
            }

            SearchDefaultSettings searchDef = new SearchDefaultSettings
                {ID = id, Name = n, Count = Int32.Parse(c), Tag = t};

            return (searchDef, message);
        }
        
        private async Task SetGameStatus()
        {
            await _client.SetGameAsync(_universe.Name + " Loaded - " +
                                       _universe.Stars.Count + " Stars - " +
                                       _universe.Planets.Count + " Planets - " +
                                       _universe.Characters.Count + " Characters");
        }

        string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}