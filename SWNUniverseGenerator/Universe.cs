﻿using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator
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
    }
}