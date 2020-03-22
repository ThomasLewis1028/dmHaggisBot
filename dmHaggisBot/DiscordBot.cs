using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
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

        //Get the token out of the properties folder
        private static readonly string Token = (string) Prop.GetValue("token");

        private readonly Regex _charChreate = new Regex("^(charCreate|createChar|cc)($| .*)", RegexOptions.IgnoreCase);

        private readonly Regex _starCreate =
            new Regex("^(starCreate|createStar|sc|cs)($| .*)", RegexOptions.IgnoreCase);

        private readonly Regex _planCreate =
            new Regex("^(planetCreate|createPlanet|pc|cp)($| .*)", RegexOptions.IgnoreCase);

        private readonly Regex _univChreate =
            new Regex("^(univCreate|createUniv|uc|cu)($| .*)", RegexOptions.IgnoreCase);

        private readonly Regex _univLoad = new Regex("^(univLoad|loadUniv|ul|lu)($| .*)", RegexOptions.IgnoreCase);

        private readonly Regex _dataSearch =
            new Regex("^(dataSearch|searchData|ds|sd)($| .*)", RegexOptions.IgnoreCase);

        private readonly Regex _probCreate =
            new Regex("^(probCreate|createProb|prc|cpr)($| .*)", RegexOptions.IgnoreCase);

        private static Emoji rightArrow = new Emoji("▶️");
        private static Emoji leftArrow = new Emoji("◀️");

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

            if (_probCreate.IsMatch(sm.Content))
            {
                if (_universe != null)
                    CreateProb(sm);
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
            var user = _client.GetUser(sr.UserId);
            if (user.IsBot)
                return;

            bool up = true;

            if (!_dataSearch.IsMatch(sr.Message.ToString()))
                return;

            if (sr.Emote.ToString().Equals(rightArrow.ToString()))
                up = true;

            else if (sr.Emote.ToString().Equals(leftArrow.ToString()))
                up = false;

            var results = ParsePagination(sr.Message.ToString(), up);
            var searchDefaultSettings = results.Item1;
            var message = results.Item2;

            await sr.Message.Value.RemoveReactionAsync(sr.Emote, user);
            await SearchData(sr.Channel, message, searchDefaultSettings, cacheable.Value);
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
                await SetGameStatus();
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
                await SetGameStatus();
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
                ? -1
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
                await SetGameStatus();
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
                await SetGameStatus();
            }
            catch (FileNotFoundException e)
            {
                await sm.Channel.SendMessageAsync(e.Message);
            }
        }

        public async Task CreateProb(SocketMessage sm)
        {
            var c = ParseCommand("c", sm.Content);
            var a = ParseCommand("a", sm.Content);
            var id = ParseCommand("id", sm.Content);

            var probDef = new ProblemDefaultSettings();

            probDef.Count = string.IsNullOrEmpty(c)
                ? -1
                : Int32.Parse(c);
            probDef.Additive = a.ToUpper() == "Y";
            probDef.ID = string.IsNullOrEmpty(id)
                ? null
                : id;

            try
            {
                _universe = _creation.CreateProblems(_universe, probDef);
                await sm.Channel.SendMessageAsync(c + " problems created in " + _universe.Name);
                await SetGameStatus();
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

            searchDef.ID = string.IsNullOrEmpty(id)
                ? new string[] { }
                : id.Split(", ");
            searchDef.Name = string.IsNullOrEmpty(n)
                ? new string[] { }
                : n.Split(", ");
            searchDef.Index = string.IsNullOrEmpty(c)
                ? 0
                : Int32.Parse(c);
            searchDef.Tag = string.IsNullOrEmpty(t)
                ? new string[] { }
                : t.Split(" ");

            await SearchData(sm.Channel, sm.Content, searchDef);
        }

        private async Task SearchData(ISocketMessageChannel sc, string sm,
            SearchDefaultSettings searchDefaultSettings, IUserMessage userMessage = null)
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

                var message = sm + " - [" + results.CurrentIndex + ", " + results.MaxCount + "]";

                if (userMessage == null)
                {
                    RestUserMessage rsu = await sc.SendMessageAsync(message, false, embeds[0]);
                    await rsu.AddReactionAsync(leftArrow);
                    await rsu.AddReactionAsync(rightArrow);
                }
                else
                {
                    await userMessage.ModifyAsync(x => x.Content = message);
                    await userMessage.ModifyAsync(x => x.Embed = embeds[0]);
                }
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
                return null;

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

            SearchDefaultSettings searchDef = new SearchDefaultSettings();
            // {ID = id, Name = n, Index = Int32.Parse(c), Tag = t}

            searchDef.ID = string.IsNullOrEmpty(id)
                ? new string[] { }
                : id.Split(", ");
            searchDef.Name = string.IsNullOrEmpty(n)
                ? new string[] { }
                : n.Split(", ");
            searchDef.Index = Int32.Parse(c);
            searchDef.Tag = string.IsNullOrEmpty(t)
                ? new string[] { }
                : t.Split(" ");
            

            return (searchDef, message);
        }

        private async Task SetGameStatus()
        {
            await _client.SetGameAsync(_universe.Name + " Loaded - " +
                                       _universe.Stars.Count + " Stars - " +
                                       _universe.Planets.Count + " Planets - " +
                                       _universe.Characters.Count + " Characters");
        }
    }
}