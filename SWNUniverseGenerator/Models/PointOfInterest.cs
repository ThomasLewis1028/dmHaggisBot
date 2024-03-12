using System;

namespace SWNUniverseGenerator.Models
{
    /// <summary>
    /// This class stores all of the necessary information about a Point of Interest
    /// </summary>
    public class PointOfInterest : BaseEntity, ILocation
    {

        /// <summary>
        /// The name of the point of interest
        /// </summary>
        public String Name { get; set; }
        
        /// <summary>
        /// The type of point of interest
        /// </summary>
        public String Type { get; set; }
        
        /// <summary>
        /// The current occupants of a point of interest
        /// </summary>
        public String OccupiedBy { get; set; }
        
        /// <summary>
        /// The situation of a given point of interest
        /// </summary>
        public String Situation { get; set; }

        /// <summary>
        /// A string value for the universe a given PointOfInterest is tied to
        /// </summary>
        public String UniverseId { get; set; }
        
        public String ZoneId { get; set; }
    }
}