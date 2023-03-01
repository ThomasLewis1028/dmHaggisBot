using System;
using System.Collections.Generic;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.Models
{
    public class ShipPreset : BaseEntity
    {
        public String PresetName { get; set; }

        public Int32 CrewSkill { get; set; }

        public Int32 Cp { get; set; }

        public List<String> Weapons { get; set; }

        public List<String> Defenses { get; set; }

        public List<String> Fittings { get; set; }
    }
}