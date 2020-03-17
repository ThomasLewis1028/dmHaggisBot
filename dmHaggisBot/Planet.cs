namespace dmHaggisBot
{
    public class Planet
    {
        private string name;

        public Planet(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }
    }
}