using System;
using System.IO;
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
        private readonly Regex charChreate = new Regex("^charCreate ");
        private readonly Regex findChar = new Regex("^findChar ");
        private readonly Regex starCreate = new Regex("^starCreate ");
        private readonly Regex univChreate = new Regex("^univCreate ");
        private readonly Regex univLoad = new Regex("^univLoad ");
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
                    sm.Channel.SendMessageAsync("No universe file loaded");
            }

            if (univChreate.IsMatch(sm.Content))
                CreateUniv(sm);

            if (univLoad.IsMatch(sm.Content))
                LoadUniv(sm);

            if (findChar.IsMatch(sm.Content))
                CharFind(sm);

            if (starCreate.IsMatch(sm.Content))
                CreateStar(sm);
        }

        public static async void CreateChar(SocketMessage sm)
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
            await Task.Delay(-1);
        }

        public static async void CreateUniv(SocketMessage sm)
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
            }
            catch (IOException e)
            {
                await sm.Channel.SendMessageAsync(e.ToString());
            }

            await Task.Delay(-1);
        }

        public static async void CreateStar(SocketMessage sm)
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


            await Task.Delay(-1);
        }

        public static async void LoadUniv(SocketMessage sm)
        {
            var n = ParseCommand("n", sm.Content);

            try
            {
                _creation = new Creation(universePath);
                _universe = _creation.LoadUniverse(n);
                await sm.Channel.SendMessageAsync("Loaded Universe with the name " + _universe.Name);
            }
            catch (FileNotFoundException e)
            {
                await sm.Channel.SendMessageAsync(e.ToString());
            }

            await Task.Delay(-1);
        }

        public static async void CharFind(SocketMessage sm)
        {
            
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
    }
}