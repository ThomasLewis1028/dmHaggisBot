using System;

namespace SWNUniverseGenerator.Models
{
    /// <summary>
    /// The base interface for a Location that also implements IEntity
    /// </summary>
    public interface ILocation : IEntity
    {
        /// <summary>
        /// ID from IEntity that all ILocation type objects must implement
        /// </summary>
        public new String Id { get; set; }

        /// <summary>
        /// All ILocation type objects much implement a Name 
        /// </summary>
        public new String Name { get; set; }
    }
}