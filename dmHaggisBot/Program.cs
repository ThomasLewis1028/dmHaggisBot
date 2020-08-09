using System;

namespace dmHaggisBot
{
    internal class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private static void Main(string[] args)
        {
            try
            {
                var bot = new DiscordBot(args.Length > 0 && args[0] == "-test");
                bot?.MainAsync();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            while (true)
            {
            }
        }
    }
}