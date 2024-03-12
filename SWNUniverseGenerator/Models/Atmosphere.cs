using System;

namespace SWNUniverseGenerator.Models
{
    /// <summary>
    /// This holds information about an Atmosphere
    /// </summary>
    public class Atmosphere : BaseEntity
    {
        /// <summary>
        /// Stores the type of Atmosphere
        /// </summary>
        public String Type { get; set; }

        /// <summary>
        /// Stores the Description of the Atmosphere
        /// </summary>
        public String Description { get; set; }
    }
}