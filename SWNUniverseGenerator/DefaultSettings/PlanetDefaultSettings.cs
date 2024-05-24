using System;
using System.Collections.Generic;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.DefaultSettings
{
    /// <summary>
    /// This holds the parameters that can be used in creating a Planet
    /// </summary>
    public class PlanetDefaultSettings
    {
        public PlanetDefaultSettings(
            string universeId = null,
            List<Star> starList = null,
            Int64 population = -1)
        {
            UniverseId = universeId;
            Name = null;
            StarList = starList;
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
        public List<Star> StarList { get; set; }
        
        public string UniverseId { get; set; }
        
        public Int64 Population { get; set; }
    }
}