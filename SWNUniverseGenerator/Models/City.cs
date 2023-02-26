using System;

namespace SWNUniverseGenerator.Models
{
    public class City : ILocation
    {
        public City()
        {
            Id = this.GenerateId();
        }
        
        public City(String name, String planetId)
        {
            Id = this.GenerateId();
            Name = name;
            PlanetId = planetId;
        }   
        
        public String Id { get; set; }

        public String Name { get; set; }

        public String PlanetId { get; }

        public Int32 Pop { get; set; }
    }
}