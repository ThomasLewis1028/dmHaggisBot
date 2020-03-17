using System.Collections.Generic;

namespace dmHaggisBot
{
    public class Universe
    {
        private Grid grid;
        private List<Star> stars = new List<Star>();
        private List<Person> people = new List<Person>();
        // private Jobs jobs;

        public Grid Grid
        {
            get => grid;
            set => grid = value;
        }

        public List<Star> Stars
        {
            get => stars;
            set => stars = value;
        }

        public List<Person> People
        {
            get => people;
            set => people = value;
        }
    }
}