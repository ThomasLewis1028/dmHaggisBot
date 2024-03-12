using System;

namespace SWNUniverseGenerator.Models
{
    /// <summary>
    /// This holds information about a Tech Level
    /// </summary>
    public class TechLevel : BaseEntity
    {
        /// <summary>
        /// Stores the type of Tech Level
        /// </summary>
        public String Type { get; set; }

        /// <summary>
        /// Stores the Short Description of the Tech Level
        /// </summary>
        public String ShortDesc { get; set; }

        /// <summary>
        /// Stores the Long Description of the Tech Level
        /// </summary>
        public String Description { get; set; }
    }
}