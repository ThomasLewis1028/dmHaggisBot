using System;

namespace SWNUniverseGenerator
{
    public class Star : ILocation
    {
        public Star(string name, int x, int y)
        {
            Name = name;
            X = x;
            Y = y;
        }

        public String ID { get; set; }

        public string Name { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}