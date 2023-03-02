using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator.Models
{
    /// <summary>
    /// This is where the World Tags from Stars Without Number's rulebook are deserialized to
    /// </summary>
    public class WorldTag : BaseEntity
    {
        /// <summary>
        /// Stores the type of World Tag
        /// </summary>
        public String Type { get; set; }

        /// <summary>
        /// Stores the Description of the World Tag
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// Stores a list of potential Enemies
        /// </summary>
        public List<String> Enemies { get; set; }

        /// <summary>
        /// Stores a list of potential Friends
        /// </summary>
        public List<String> Friends { get; set; }

        /// <summary>
        /// Stores a list of potential Complications
        /// </summary>
        public List<String> Complications { get; set; }

        /// <summary>
        /// Stores a list of potential Things to find
        /// </summary>
        public List<String> Things { get; set; }

        /// <summary>
        /// Stores a list of potential Places
        /// </summary>
        public List<String> Places { get; set; }
    }
}