using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWNUniverseGenerator.Models
{
    /// <summary>
    /// The Universe class that holds all information about a Universe
    /// </summary>
    public class Universe: BaseEntity
    {
        /// <summary>
        /// Default constructor to create a blank universe
        /// </summary>
        public Universe() : base()
        {
            Name = null;
            GridX = -1;
            GridY = -1;
        }

        /// <summary>
        /// The Name of a Universe
        /// To be used with setting a fileName.
        /// </summary>
        public String Id { get; set; }
        
        /// <summary>
        /// The Name of a Universe
        /// To be used with setting a fileName.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The Grid Width of the Universe
        /// </summary>
        public Int32 GridX { get; set; }
        
        /// <summary>
        /// The Grid Height of the Universe
        /// </summary>
        public Int32 GridY { get; set; }
    }
}