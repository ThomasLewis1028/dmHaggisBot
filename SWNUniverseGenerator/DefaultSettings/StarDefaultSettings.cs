using System;

namespace SWNUniverseGenerator.DefaultSettings
{
    /// <summary>
    /// This holds the parameters that can be used in creating a Star
    /// </summary>
    public class StarDefaultSettings
    {
        /// <summary>
        /// Store the number of stars you want to create
        /// </summary>
        public Int32 StarCount { get; set; }
        
        /// <summary>
        /// Store the Name of a Star you would like to create
        /// Cannot be used with a StarCount
        /// </summary>
        public String Name { get; set; }
        
        /// <summary>
        /// Store a Grid location for the star.
        /// </summary>
        public Grid Grid { get; set;  }
    }
}