using System;

namespace SWNUniverseGenerator.Models
{
    public class ShipFitting : BaseEntity
    {
        public String ShipId { get; set; }
        
        public String FittingId { get; set; }
        
        public String UniverseId { get; set; }
    }
}