using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SWNUniverseGenerator.Models
{
    /// <summary>
    /// The Universe class that holds all information about a Universe
    /// </summary>
    public class Universe
    {
        /// <summary>
        /// All Universes must be instantiated with a Name and a Grid to create all other information
        /// </summary>
        /// <param name="name"></param>
        /// <param name="grid"></param>
        public Universe(string name, Grid grid)
        {
            Name = name;
            Grid = grid;
        }

        /// <summary>
        /// The Name of a Universe
        /// To be used with setting a fileName.
        /// </summary>
        public String Name { get; set; }
        
        /// <summary>
        /// The Grid size of the Universe
        /// </summary>
        public Grid Grid { get; set; }
        
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

    public class Zone
    {
        public Int32 X { get; set; }
        
        public Int32 Y { get; set; }
        
        [JsonIgnore]
        public String GetHex => (X < 10 ? "0" + X : X.ToString()) +
                                 (Y < 10 ? "0" + Y : Y.ToString());
        
        public String StarID { get; set; }
    }
}