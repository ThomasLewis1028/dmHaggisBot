using System;

namespace SWNUniverseGenerator.Models
{
    /// <summary>
    /// This holds information about a Temperature
    /// </summary>
    public class Temperature : BaseEntity
    {
        /// <summary>
        /// Stores the type of Temperature
        /// </summary>
        public String Type { get; set; }

        /// <summary>
        /// Stores the Description of the Temperature
        /// </summary>
        public String Description { get; set; }
    }
}