using System.Collections.Generic;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.DeserializedObjects
{
    public class ShipData : BaseEntity
    {
        public List<ShipHullObject> Hulls { get; set; }

        public List<ShipFittingObject> Fittings { get; set; }

        public List<ShipDefenseObject> Defenses { get; set; }

        public List<ShipWeaponObject> Weapons { get; set; }

        public List<ShipPresets> Presets { get; set; }

        public ShipNaming Naming { get; set; }
    }
}