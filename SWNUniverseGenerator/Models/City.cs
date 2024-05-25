using System;

namespace SWNUniverseGenerator.Models
{
    public class City : ILocation
    {
        public City()
        {
            Id = this.GenerateId();
        }
        
        public City(String name, String planetId, String universeId)
        {
            Id = this.GenerateId();
            Name = name;
            PlanetId = planetId;
            UniverseId = universeId;
        }   
        
        public String Id { get; set; }

        public String Name { get; set; }

        public String PlanetId { get; set; }

        public Int64 Population { get; set; }
        
        public String UniverseId { get; set; }
    }
}