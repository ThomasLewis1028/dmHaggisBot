using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator.Models
{
    public class Alien : IEntity
    {
        public String Id { get; set; }
        
        public String Name { get; set; }
        
        public String BodyTraits { get; set; }
        
        public String Lenses { get; set; }
        
        public String SocialStructures { get; set; }
        
        public String MultiPolarType { get; set; }
    }
}