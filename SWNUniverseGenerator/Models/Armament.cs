using System;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.Models
{
    public class Armament : BaseEntity
    {
        public String Type { get; set; }

        public Int32 Cost { get; set; }

        public Int32? AmmoCost { get; set; }

        public String Dmg { get; set; }

        public Int32 Power { get; set; }

        public Int32 Mass { get; set; }

        public Int32 Hardpoints { get; set; }

        public String Class { get; set; }

        public Int32 TechLevel { get; set; }

        public String Qualities { get; set; }

        public String Description { get; set; }
    }
}