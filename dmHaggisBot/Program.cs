using System;
using System.Threading.Tasks;

namespace dmHaggisBot
{
    internal static class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private static async Task Main(string[] args)
        {
            try
            {
                var bot = new DiscordBot(args.Length > 0 && args[0] == "-test");
                await bot.MainAsync();

                while (true)
                {
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}