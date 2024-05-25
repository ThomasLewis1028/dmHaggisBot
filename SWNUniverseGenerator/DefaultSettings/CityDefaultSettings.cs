using System;
using System.Collections.Generic;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.DefaultSettings
{
    /// <summary>
    /// This holds the parameters that can be used in creating a Planet
    /// </summary>
    public class CityDefaultSettings
    {
        public CityDefaultSettings(
            string universeId = null,
            List<Planet> planetList = null,
            long population = -1)
        {
            UniverseId = universeId;
            Name = null;
            PlanetList = planetList;
            Population = population;
        }

        /// <summary>
        /// Store the Name of a Planet for creation
        /// Not to be used with PlanetRange
        /// </summary>
        public String Name { get; set; }
        
        /// <summary>
        /// Store a list of StarIDs if you want to create a planet on a specific Star or set of Stars
        /// By default, it will generate planets for each star
        /// </summary>
        public List<Planet> PlanetList { get; set; }
        
        public string UniverseId { get; set; }
        
        public long Population { get; set; }
    }
}