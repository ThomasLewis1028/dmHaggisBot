using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;

namespace dmHaggisBot
{
    class DiscordBot
    {
        private DiscordSocketClient _client;
        private IConfiguration _config;
        private Creation creation;
        private Regex charChreate = new Regex("^charCreate ");
        private Regex univChreate = new Regex("^univCreate ");
        private static Universe _universe;
        private static Creation _creation;

        //Properties file
        private static readonly JObject prop =
            JObject.Parse(
                File.ReadAllText(@"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\dmHaggisBot\properties.json"));

        private static string token = (string) prop.GetValue("token");

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.MessageReceived += MessageReceived;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        public async Task MessageReceived(SocketMessage sm)
        {
            if (sm.Author.IsBot)
                return;

            if (charChreate.IsMatch(sm.Content))
                CreateChar(sm);

            if (univChreate.IsMatch(sm.Content))
                CreateUniv(sm);
        }

        public async static void CreateChar(SocketMessage sm)
        {
            var a = ParseCommand("a", sm.Content);
            var ar = a.Split(" ").Length == 1 
                ? new[] {a, a} : 
                a.Split(" ");
            var c = ParseCommand("c", sm.Content);
            var g = ParseCommand("g", sm.Content);
            var f = ParseCommand("f", sm.Content);
            var l = ParseCommand("l", sm.Content);
            var h = ParseCommand("h", sm.Content);
            var hc = ParseCommand("hc", sm.Content);
            var ec = ParseCommand("ec", sm.Content);
            var t = ParseCommand("t", sm.Content);
            
            // await sm.Channel.SendMessageAsync(sb.ToString());
            CharacterDefaultSettings charDef = new CharacterDefaultSettings();

            charDef.Count = c != "" ? Int32.Parse(c) : 1;
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

        public async static void CreateUniv(SocketMessage sm)
        {
            var g = ParseCommand("g", sm.Content);
            var n = ParseCommand("n", sm.Content);
            var o = ParseCommand("o", sm.Content);
            
            UniverseDefaultSettings univDef = new UniverseDefaultSettings();

            univDef.Name = n;
            univDef.Grid = g;
            univDef.Overwrite = o;

            try
            {
                _creation = new Creation();
                _universe = _creation.CreateUniverse(univDef);
            }
            catch (FileLoadException e)
            {
                sm.Channel.SendMessageAsync(e.ToString());
            }

            await sm.Channel.SendMessageAsync("Created Universe with the name " + _universe.Name);
            await Task.Delay(-1);
        }

        private static string ParseCommand(string argName, string argVal)
        {
            var start = argVal.IndexOf(" -" + argName) + 1;
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