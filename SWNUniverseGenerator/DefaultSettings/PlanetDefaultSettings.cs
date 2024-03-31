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
            Tuple<Int32, Int32> planetRange = null, 
            List<Star> starList = null)
        {
            UniverseId = universeId;
            PlanetRange = planetRange ?? new Tuple<Int32, Int32>(0, 3);
            Name = null;
            StarList = starList;
        }
        /// <summary>
        /// Store an upper and lower bound on the number of planets to be created
        /// </summary>
        public Tuple<Int32, Int32> PlanetRange { get; set; }

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
    }
}