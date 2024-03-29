using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator.Models
{
    /// <summary>
    /// This class stores all of the necessary information about a Point of Interest
    /// </summary>
    public class PoiData : BaseEntity
    {
       
        /// <summary>
        /// The type of point of interest
        /// </summary>
        public String Type { get; set; }
        
        /// <summary>
        /// List of occupation types for a given type
        /// </summary>
        public List<String> OccupiedBy { get; set; }
        
        /// <summary>
        /// List of situations for a given type
        /// </summary>
        public List<String> Situation { get; set; }
    }
}