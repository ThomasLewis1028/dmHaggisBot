using System.Collections.Generic;

namespace SWNUniverseGenerator
{
    public class Universe
    {
        // private Jobs jobs;

        public Universe(string name, Grid grid)
        {
            Name = name;
            Grid = grid;
        }

        public string Name { get; set; }
        public Grid Grid { get; set; }
        public List<Star> Stars { get; set; }
        public List<Planet> Planets { get; set; }
        public List<Character> Characters { get; set; }
        public List<Problem> Problems { get; set; }
    }
}