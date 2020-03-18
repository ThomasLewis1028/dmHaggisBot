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
                createChar(sm);

            if (univChreate.IsMatch(sm.Content))
                createUniv(sm);
        }

        public async static void createChar(SocketMessage sm)
        {
            var a = parseCommand("a", sm.Content);
            var ar = a.Split(" ");
            var c = parseCommand("c", sm.Content);
            var g = parseCommand("g", sm.Content);
            var f = parseCommand("f", sm.Content);
            var l = parseCommand("l", sm.Content);
            var h = parseCommand("h", sm.Content);
            var hc = parseCommand("hc", sm.Content);
            var ec = parseCommand("ec", sm.Content);
            var t = parseCommand("t", sm.Content);

            Console.Out.WriteLine("a - {0}, c - {1}, g - {2}", string.Join(" ", ar), c, g);

            // await sm.Channel.SendMessageAsync(sb.ToString());
            // CharCreation charCreation = new CharCreation(ar, c, g, f, l, h, hc, ec, t);
            await Task.Delay(-1);
        }
        
        public async static void createUniv(SocketMessage sm)
        {
            var g = parseCommand("g", sm.Content);
            var gp = g.Split(" ");
            var sc = parseCommand("sc", sm.Content);
            var pm = parseCommand("pm", sm.Content);
            var pc = parseCommand("pc", sm.Content);

            // await sm.Channel.SendMessageAsync(sb.ToString());
            // CharCreation charCreation = new CharCreation(ar, c, g, f, l, h, hc, ec, t);
            await Task.Delay(-1);
        }

        private static string parseCommand(string argName, string argVal)
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