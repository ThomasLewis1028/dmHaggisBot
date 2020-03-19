using System;
using System.Collections.Generic;

namespace dmHaggisBot
{
    public class Universe
    {
        public String Name { get; set; }
        public Grid Grid { get; set; }
        public List<Star> Stars { get; set;}
        public List<Character> Characters { get; set; }
        // private Jobs jobs;

        public Universe(string name, Grid grid)
        {
            Name = name;
            Grid = grid;
        }
    }
}