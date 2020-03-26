using System;
using Newtonsoft.Json;

namespace SWNUniverseGenerator.Models
{
    /// <summary>
    /// This holds all of the necessary information about a Star
    /// </summary>
    public class Star : ILocation
    {
        /// <summary>
        /// A unique ID that will be generated when creating a Star
        /// </summary>
        public String ID { get; set; }

        /// <summary>
        /// The string Name of a Star
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// A Star's X location on a Grid
        /// </summary>
        public Int32 X { get; set; }

        /// <summary>
        /// A Star's Y location on a Grid
        /// </summary>
        public Int32 Y { get; set; }

        /// <summary>
        /// The specific zone location on a grid
        /// </summary>
        [JsonIgnore] public String GetZone => X.ToString() + Y.ToString() ;
    }
}