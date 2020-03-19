using System;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dmHaggisBot
{
    class Program
    {
        static void Main(string[] args)
        {
            DiscordBot bot = new DiscordBot();
            bot.MainAsync();

            while (true)
            {
            }
        }
    }
}