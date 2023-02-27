using System.Collections.Generic;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.DeserializedObjects
{
    public class ShipData : BaseEntity
    {
        public List<ShipHull> Hulls { get; set; }

        public List<ShipFitting> Fittings { get; set; }

        public List<ShipDefense> Defenses { get; set; }

        public List<ShipWeapon> Weapons { get; set; }

        public List<ShipPresets> Presets { get; set; }

        public ShipNaming Naming { get; set; }
    }
}