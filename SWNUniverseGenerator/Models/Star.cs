using System;
using Newtonsoft.Json;

namespace SWNUniverseGenerator.Models
{
    /// <summary>
    /// This holds all of the necessary information about a Star
    /// </summary>
    public class Star : ILocation
    {
        /// <summary>
        /// A unique ID that will be generated when creating a Star
        /// </summary>
        public String Id { get; set; }

        /// <summary>
        /// The string Name of a Star
        /// </summary>
        public String Name { get; set; }
        
        /// <summary>
        /// The Type of Star
        /// </summary>
        public String StarType { get; set; }
    }
}