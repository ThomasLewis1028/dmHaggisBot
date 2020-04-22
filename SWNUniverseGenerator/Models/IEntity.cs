using System;

namespace SWNUniverseGenerator.Models
{
    /// <summary>
    /// The base Interface for all entities in my program. Anything that has an ID must implement this.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// All IEntity objects must have an ID
        /// </summary>
        public String Id { get; set; }
        
        /// <summary>
        /// All IEntity objects must have a Name
        /// </summary>
        public String Name { get; }
    }
}