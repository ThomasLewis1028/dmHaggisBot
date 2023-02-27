using System;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.Models
{
    public class ShipHull : BaseEntity
    {
        public String Type { get; set; }

        public Int32 Cost { get; set; }

        public Int32? Speed { get; set; }

        public Int32 Armor { get; set; }

        public Int32 Hp { get; set; }

        public Int32 CrewMin { get; set; }

        public Int32 CrewMax { get; set; }

        public Int32 Ac { get; set; }

        public Int32 Power { get; set; }

        public Int32 Mass { get; set; }

        public Int32 Hardpoints { get; set; }

        public String Class { get; set; }

        public String Description { get; set; }
    }
}