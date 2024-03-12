using System;

namespace SWNUniverseGenerator.Models
{
    public class ShipDefense : BaseEntity
    {
        public String ShipId { get; set; }
        
        public String DefenseId { get; set; }
        
        public String UniverseId { get; set; }
    }
}