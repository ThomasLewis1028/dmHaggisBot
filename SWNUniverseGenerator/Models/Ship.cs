using System;
using System.Collections.Generic;
using SWNUniverseGenerator.DeserializedObjects;

namespace SWNUniverseGenerator.Models
{
    public class Ship : IEntity
    {
        public String ID { get; set; }
        
        public String Name { get; set; }
        
        public Hull Hull { get; set; }
        
        public List<Fitting> Fittings { get; set; }
        
        public List<Defense> Defenses { get; set; }
        
        public List<Weapon> Weapons { get; set; }
    }
}