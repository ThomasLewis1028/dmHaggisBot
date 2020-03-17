namespace dmHaggisBot
{
    public class Universe
    {
        private Grid grid;
        private Stars stars;
        private People people;
        // private Jobs jobs;

        public Grid Grid
        {
            get => grid;
            set => grid = value;
        }

        public Stars Stars
        {
            get => stars;
            set => stars = value;
        }

        public People People
        {
            get => people;
            set => people = value;
        }
    }
}