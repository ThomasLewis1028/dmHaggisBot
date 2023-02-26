using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text.Json.Serialization;

namespace SWNUniverseGenerator.Models
{
    /// <summary>
    /// The Universe class that holds all information about a Universe
    /// </summary>
    public class Universe
    {
        /// <summary>
        /// Default constructor to create a blank universe
        /// </summary>
        public Universe()
        {
            Name = null;
            GridX = -1;
            GridY = -1;
        }
        
        /// <summary>
        /// All Universes must be instantiated with a Name and a Grid to create all other information
        /// </summary>
        /// <param name="name"></param>
        /// <param name="grid"></param>
        public Universe(string name, int x, int y)
        {
            Name = name;
            GridX = x;
            GridY = y;
        }

        /// <summary>
        /// The Name of a Universe
        /// To be used with setting a fileName.
        /// </summary>
        [Key]
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

    public class Zone : IEntity
    {
        [JsonIgnore]
        public String GetHex => (X < 10 ? "0" + X : X.ToString()) +
                            (Y < 10 ? "0" + Y : Y.ToString());

        public String Id { get; set; }
        
        [JsonIgnore] public String Name => GetHex;

        public Int32 X { get; set; }

        public Int32 Y { get; set; }

        public String StarId { get; set; }
        
        public List<Planet> Planets { get; set; }

        public List<PointOfInterest> PointsOfInterest { get; set; }
    }
}