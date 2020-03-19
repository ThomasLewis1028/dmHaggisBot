using System.Collections.Generic;

namespace SWNUniverseGenerator
{
    public class Star
    {
        public Star(string name, int x, int y)
        {
            Name = name;
            X = x;
            Y = y;
        }

        public string Name { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public List<Planet> Planets { get; set; } = new List<Planet>();
    }
}