using System;

namespace SWNUniverseGenerator.DefaultSettings
{
    public class ProblemDefaultSettings
    {
        public ProblemDefaultSettings()
        {
            Count = 5;
            Additive = true;
            LocationId = null;
        }
        
        public Int32 Count { get; set; }
        
        public Boolean Additive { get; set; }
        
        public String LocationId { get; set; }
    }
}