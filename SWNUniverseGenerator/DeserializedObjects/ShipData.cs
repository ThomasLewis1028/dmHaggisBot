using System;
using System.Collections.Generic;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.DeserializedObjects
{
    [Obsolete]
    public class ShipData : BaseEntity
    {
        public List<Hull> Hulls { get; set; }

        public List<Fitting> Fittings { get; set; }

        public List<Defense> Defenses { get; set; }

        public List<Armament> Weapons { get; set; }

        public List<ShipPresets> Presets { get; set; }

        public ShipNaming Naming { get; set; }
    }
}