namespace dmHaggisBot
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bot = new DiscordBot();
            bot?.MainAsync();

            while (true)
            {
            }
        }
    }
}