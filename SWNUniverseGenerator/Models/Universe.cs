using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator.Models
{
    /// <summary>
    /// The Universe class that holds all information about a Universe
    /// </summary>
    public class Universe: IEntity
    {
        /// <summary>
        /// Default constructor to create a blank universe
        /// </summary>
        public Universe()
        {
            Id = this.GenerateId();
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
        
        /// <summary>
        /// A list of all zones in the universe
        /// </summary>
        public List<Zone> Zones { get; set; }

        /// <summary>
        /// The list of all Stars in the Universe
        /// </summary>
        public List<Star> Stars { get; set; }

        /// <summary>
        /// The list of all Planets in the Universe
        /// </summary>
        public List<Planet> Planets { get; set; }

        /// <summary>
        /// The list of all Characters in the Universe
        /// </summary>
        public List<Character> Characters { get; set; }
        
        /// <summary>
        /// The list of all Aliens in the Universe
        /// </summary>
        public List<Alien> Aliens { get; set; }
        
        /// <summary>
        /// The list of all Ships in the Universe
        /// </summary>
        public List<Ship> Ships { get; set; }

        /// <summary>
        /// The list of all Problems in the Universe
        /// </summary>
        public List<Problem> Problems { get; set; }

        /// <summary>
        /// The list of all Jobs in the Universe
        /// </summary>
        public List<Job> Jobs { get; set; }

        /// <summary>
        /// The list of all Points of Interest in the Universe
        /// </summary>
        public List<PointOfInterest> PointsOfInterest { get; set; }
    }
}