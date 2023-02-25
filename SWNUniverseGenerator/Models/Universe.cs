using System;
using System.Collections.Generic;
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
            Grid = null;
        }
        
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
        
        /// <summary>
        /// A single Bitmap for the StarMap as a whole
        /// </summary>
        public String StarMap { get; set; }
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

        public List<String> Planets { get; set; }

        public List<String> PointsOfInterest { get; set; }
    }

    public class Grid
    {
        /// <summary>
        /// Constructor requires an X and Y value to create the grid
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Grid(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// The X length of a grid
        /// </summary>
        public Int32 X { get; set; }

        /// <summary>
        /// The Y length of a grid
        /// </summary>
        public Int32 Y { get; set; }
    }
}