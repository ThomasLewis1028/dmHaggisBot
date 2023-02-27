using System;
using System.Collections.Generic;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.Models
{
    public class ShipPresets : BaseEntity
    {
        public String HullType { get; set; }

        public List<ShipPreset> ListPresets { get; set; }
    }
}