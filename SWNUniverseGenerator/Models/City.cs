using System;

namespace SWNUniverseGenerator.Models
{
    public class City : ILocation
    {
        public String Id { get; set; }

        public City(String name, String planetId)
        {
            Name = name;
            PlanetId = planetId;
        }

        public String Name { get; set; }

        public String PlanetId { get; }

        public Int32 Pop { get; set; }
    }
}