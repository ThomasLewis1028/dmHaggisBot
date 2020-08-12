using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator.Models
{
    public class Alien : IEntity
    {
        public String Id { get; set; }
        
        public String Name { get; set; }
        
        public List<String> BodyTraits { get; set; }
        
        public List<String> Lenses { get; set; }
        
        public List<String> SocialStructures { get; set; }
        
        public String MultiPolarType { get; set; }
    }
}