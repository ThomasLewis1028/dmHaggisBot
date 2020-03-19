using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        private static readonly JObject prop =
            JObject.Parse(
                File.ReadAllText(@"properties.json"));

        private static readonly string token = (string) prop.GetValue("token");
        private readonly Regex charChreate = new Regex("^(charCreate|createChar|cc)($|.*)", RegexOptions.IgnoreCase);
        private readonly Regex starCreate = new Regex("^(starCreate|createStar|sc|cs)($|.*)", RegexOptions.IgnoreCase);
        private readonly Regex univChreate = new Regex("^(univCreate|createUniv|uc|cu)($|.*)", RegexOptions.IgnoreCase);
        private readonly Regex univLoad = new Regex("^(univLoad|loadUniv|ul|lu)($|.*)", RegexOptions.IgnoreCase);
        private readonly Regex dataSearch = new Regex("^(dataSearch|searchData|ds|sd)($|.*)", RegexOptions.IgnoreCase);
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
            _client = new DiscordSocketClient();
            _client.MessageReceived += MessageReceived;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await _client.SetGameAsync("No Universe Loaded");

            Console.Out.WriteLine("DiscordBot Connected");

            await Task.Delay(-1);
        }

        public async Task MessageReceived(SocketMessage sm)
        {
            if (sm.Author.IsBot)
                return;

            if (charChreate.IsMatch(sm.Content))
            {
                if (_universe != null)
                    CreateChar(sm);
                else
                    await sm.Channel.SendMessageAsync("No universe file loaded");
            }

            if (univChreate.IsMatch(sm.Content))
                CreateUniv(sm);

            if (univLoad.IsMatch(sm.Content))
                LoadUniv(sm);

            if (starCreate.IsMatch(sm.Content))
                CreateStar(sm);

            if (dataSearch.IsMatch(sm.Content))
                SearchData(sm);
        }

        public async void CreateChar(SocketMessage sm)
        {
            var a = ParseCommand("a", sm.Content);
            var ar = string.IsNullOrEmpty(a)
                ? new[] {"", ""}
                : a.Split(" ").Length == 1
                    ? new[] {a, a}
                    : a.Split(" ");
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

            charDef.Count = c != "" ? int.Parse(c) : 1;
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

            _universe = _creation.CreateCharacter(_universe, charDef);

            await sm.Channel.SendMessageAsync(charDef.Count + " new character(s) created in " + _universe.Name);
            SetGameStatus();

            await Task.Delay(-1);
        }

        public async void CreateUniv(SocketMessage sm)
        {
            var g = ParseCommand("g", sm.Content);
            var n = ParseCommand("n", sm.Content);
            var o = ParseCommand("o", sm.Content);

            var univDef = new UniverseDefaultSettings();

            univDef.Name = n;
            univDef.Grid = g;
            univDef.Overwrite = o;

            try
            {
                _creation = new Creation(universePath);
                _universe = _creation.CreateUniverse(univDef);
                await sm.Channel.SendMessageAsync("Created Universe with the name " + _universe.Name);
                SetGameStatus();
            }
            catch (IOException e)
            {
                await sm.Channel.SendMessageAsync(e.ToString());
            }

            await Task.Delay(-1);
        }

        public async void CreateStar(SocketMessage sm)
        {
            var s = ParseCommand("s", sm.Content);
            var p = ParseCommand("p", sm.Content);
            var pr = string.IsNullOrEmpty(p)
                ? new[] {"", ""}
                : p.Split(" ").Length == 1
                    ? new[] {p, p}
                    : p.Split(" ");

            var starDef = new StarDefaultSettings();

            starDef.StarCount = s;
            starDef.PlanetRange = pr;

            _universe = _creation.CreateStars(_universe, starDef);
            await sm.Channel.SendMessageAsync(s + " stars created with varying planets in " + _universe.Name);
            SetGameStatus();

            await Task.Delay(-1);
        }

        public async void LoadUniv(SocketMessage sm)
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
                await sm.Channel.SendMessageAsync(e.ToString());
            }

            await Task.Delay(-1);
        }

        public static async void SearchData(SocketMessage sm)
        {
            var id = ParseCommand("id", sm.Content);
            var n = ParseCommand("n", sm.Content);
            var c = ParseCommand("c", sm.Content);
            var t = ParseCommand("t", sm.Content);

            SearchDefaultSettings searchDef = new SearchDefaultSettings();

            searchDef.ID = id;
            searchDef.Name = n;
            searchDef.Count = c;
            searchDef.Tag = t;

            List<IEntity> results = _creation.SearchUniverse(_universe, searchDef);
            List<Embed> embeds = new List<Embed>();

            if (results != null)
            {
                foreach (var res in results)
                {
                    if (res.GetType() == typeof(Character))
                        embeds.Add(GenerateEmbeds.CharacterEmbed((Character) res));
                    else if (res.GetType() == typeof(Planet))
                        embeds.Add(GenerateEmbeds.PlanetEmbed(_universe, (Planet) res));
                    else if (res.GetType() == typeof(Star))
                        embeds.Add(GenerateEmbeds.StarEmbed(_universe, (Star) res));
                }

                foreach (var e in embeds)
                {
                    await sm.Channel.SendMessageAsync(null, false, e);
                }
            }
            else
            {
                await sm.Channel.SendMessageAsync("No results found");
            }

            await Task.Delay(-1);
        }

        private static string ParseCommand(string argName, string argVal)
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

        private async void SetGameStatus()
        {
            await _client.SetGameAsync(_universe.Name + " Loaded\n" +
                                       _universe.Stars.Count + " Stars\n" +
                                       _universe.Planets.Count + " Planets\n" +
                                       _universe.Characters.Count + " Characters");
        }
    }
}