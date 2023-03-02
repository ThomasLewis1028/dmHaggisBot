using System;

namespace SWNUniverseGenerator.Models
{
    /// <summary>
    /// This hold information about a Biosphere
    /// </summary>
    public class Biosphere : BaseEntity
    {
        /// <summary>
        /// Stores the type of Biosphere
        /// </summary>
        public String Type { get; set; }
        
        /// <summary>
        /// Stores the Description of the Biosphere
        /// </summary>
        public String Description { get; set; }
    }
}