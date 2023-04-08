using System;

namespace SWNUniverseGenerator.Models
{
    public class LocationProblem : BaseEntity
    {
        
        public String Name => Id;

        public String LocationId { get; set; }
        
        public String ConflictType { get; set; }
        
        public String Situation { get; set; }
        
        public String Focus { get; set; }
        
        public String Restraint { get; set; }
        
        public String Twist { get; set; }
        
        /// <summary>
        /// A string value for the universe a given problem is tied to
        /// </summary>
        public String UniverseId { get; set; }
    }
}