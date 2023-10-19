using System;

namespace SWNUniverseGenerator.DefaultSettings
{
    /// <summary>
    /// This holds the parameters that can be used in creating a Planet
    /// </summary>
    public class PlanetDefaultSettings
    {
        public PlanetDefaultSettings()
        {
            PlanetRange = new []{1, 3};
            Name = null;
            StarId = null;
        }
        /// <summary>
        /// Store an upper and lower bound on the number of planets to be created
        /// </summary>
        public Int32[] PlanetRange { get; set; }

        /// <summary>
        /// Store the Name of a Planet for creation
        /// Not to be used with PlanetRange
        /// </summary>
        public String Name { get; set; }
        
        /// <summary>
        /// Store a list of StarIDs if you want to create a planet on a specific Star or set of Stars
        /// </summary>
        public String[] StarId { get; set; }
    }
}