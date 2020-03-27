using System;
using System.IO;
using System.Reflection;
using SWNUniverseGenerator;
using SWNUniverseGenerator.Models;

namespace dmHaggisBot
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bot = new DiscordBot(args[0] == "-test");
            bot?.MainAsync();

            while (true)
            {
            }
        }
    }
}