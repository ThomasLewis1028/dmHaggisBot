using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator.Models
{
    public class ShipSpec : BaseEntity
    {
        public String HullType { get; set; }
        
        public String PresetName { get; set; }
        
        public Int32 CrewSkill { get; set; }
        
        public Int32 CP { get; set; }

        public List<String> Weapons { get; set; }

        public List<String> Defenses { get; set; }

        public List<String> Fittings { get; set; }
    }
}