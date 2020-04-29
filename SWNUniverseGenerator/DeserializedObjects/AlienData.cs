using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator.DeserializedObjects
{
    public class AlienData
    {
        public List<String> BodyTraits { get; set; }
        
        public List<Lense> Lenses { get; set; }
        
        public List<SocialStructure> SocialStructures { get; set; }
    }

    public class Lense
    {
        public String Type { get; set; }
        
        public String Description { get; set; }
    }
    
    public class SocialStructure
    {
        public String Type { get; set; }
        
        public String Description { get; set; }
    }
}