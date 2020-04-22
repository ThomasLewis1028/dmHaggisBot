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