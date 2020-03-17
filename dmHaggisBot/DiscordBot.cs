using System;
using System.IO;
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
            if(!sm.Author.IsBot)
                await sm.Channel.SendMessageAsync("Testing 1 2 3");
            
        }
    }
}