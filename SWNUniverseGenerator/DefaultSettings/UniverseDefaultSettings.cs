using System;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.DefaultSettings
{
    /// <summary>
    /// This is the default creator class for Universe Objects
    /// </summary>
    public class UniverseDefaultSettings
    {
        public UniverseDefaultSettings(
            String universeId = null,
            String name = "Universe",
            int gridX = 8,
            int gridY = 10)
        {
            UniverseId = universeId;
            Name = name;
            GridX = gridX;
            GridY = gridY;
            StarDefaultSettings = new StarDefaultSettings(universeId: universeId);
            PlanetDefaultSettings = new PlanetDefaultSettings(universeId: universeId);
            CharacterDefaultSettings = new CharacterDefaultSettings(universeId: universeId);
            ShipDefaultSettings = new ShipDefaultSettings(universeId: universeId);
            PoiDefaultSettings = new PoiDefaultSettings(universeId: universeId);
        }

        /// <summary>
        /// This value should be a universe ID and should only be used sparingly
        /// </summary>
        public String UniverseId { get; set; }

        /// <summary>
        /// This value should be the name of your universe
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// This value should be an integer X for your width
        /// </summary>
        public Int32 GridX { get; set; }

        /// <summary>
        /// This value should be an integer Y for your height
        /// </summary>
        public Int32 GridY { get; set; }

        public StarDefaultSettings StarDefaultSettings { get; set; }

        public PlanetDefaultSettings PlanetDefaultSettings { get; set; }

        public ShipDefaultSettings ShipDefaultSettings { get; set; }

        public CharacterDefaultSettings CharacterDefaultSettings { get; set; }

        public PoiDefaultSettings PoiDefaultSettings { get; set; }
    }
}