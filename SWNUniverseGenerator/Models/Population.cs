using System;

namespace SWNUniverseGenerator.Models
{
    /// <summary>
    /// This holds information about a Population
    /// </summary>
    public class Population : BaseEntity
    {
        /// <summary>
        /// Stores the type of Population
        /// </summary>
        public String Type { get; set; }
        
        /// <summary>
        /// Stores the Description of the Population
        /// </summary>
        public String Description { get; set; }
    }
}