using System.Collections.Generic;

namespace dmHaggisBot
{
    public class Universe
    {
        private Grid grid;
        private List<Star> stars = new List<Star>();
        private List<Character> characters = new List<Character>();
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

        public List<Character> Characters
        {
            get => characters;
            set => characters = value;
        }
    }
}