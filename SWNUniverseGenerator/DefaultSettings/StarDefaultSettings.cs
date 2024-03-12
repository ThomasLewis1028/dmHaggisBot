using System;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.DefaultSettings
{
    /// <summary>
    /// This holds the parameters that can be used in creating a Star
    /// </summary>
    public class StarDefaultSettings
    {
        public StarDefaultSettings(Int32 starCount = -1, string name = null, Star.StarClassEnum starClass = Star.StarClassEnum.Undefined)
        {
            StarCount = starCount < 0 ? new Random().Next(0, 10) + 20 : starCount;
            Name = name;
            StarClass = starClass;
        }

        /// <summary>
        /// Store the number of stars you want to create
        /// </summary>
        public Int32 StarCount { get; set; }

        /// <summary>
        /// Store the Name of a Star you would like to create
        /// Cannot be used with a StarCount
        /// </summary>
        public String Name { get; set; }
        
        /// <summary>
        /// Store the Class of a Star you would like to create
        /// </summary>
        public Star.StarClassEnum StarClass { get; set; }
        
        /// <summary>
        /// Store the Color of a Star you would like to create
        /// </summary>
        public Star.StarColorEnum StarColor { get; set; }
    }
}