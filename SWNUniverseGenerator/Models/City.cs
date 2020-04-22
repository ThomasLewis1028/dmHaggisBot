using System;

namespace SWNUniverseGenerator.Models
{
    public class City : ILocation
    {
        public String Id { get; set; }

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