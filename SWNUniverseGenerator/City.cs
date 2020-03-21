using System;

namespace SWNUniverseGenerator
{
    public class City : IEntity
    {
        public String ID { get; set; }

        public City(string name, Planet planet)
        {
            Name = name;
            Planet = planet;
        }

        public string Name { get; set; }

        public Planet Planet { get; set; }

        public Int32 Pop { get; set; }
    }
}