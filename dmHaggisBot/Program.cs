namespace dmHaggisBot
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bot = new DiscordBot(args.Length > 0  && args[0] == "-test");
            bot?.MainAsync();

            while (true)
            {
            }
        }
    }
}