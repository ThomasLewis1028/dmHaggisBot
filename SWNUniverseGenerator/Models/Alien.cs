using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator.Models
{
    public class Alien : BaseEntity
    {
        public Alien()
        {
            Id = this.GenerateId();
        }

        public String Name { get; set; }
        
        public String BodyTraits { get; set; }
        
        public String Lenses { get; set; }
        
        public String SocialStructures { get; set; }
        
        public String MultiPolarType { get; set; }
        
        /// <summary>
        /// A string value for the universe a given alien is tied to
        /// </summary>
        public String UniverseId { get; set; }
    }
}