namespace dmHaggisBot
{
    public class City
    {
        private string name;
        private Planet planet;
        private int pop;

        public City(string name, Planet planet)
        {
            name = name;
            this.planet = planet;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public Planet Planet
        {
            get => planet;
            set => planet = value;
        }

        public int Pop
        {
            get => pop;
            set => pop = value;
        }
    }
}