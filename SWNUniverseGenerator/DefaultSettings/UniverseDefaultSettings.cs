using System;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.DefaultSettings
{
    /// <summary>
    ///     This is the default creator class for Universe Objects
    /// </summary>
    public class UniverseDefaultSettings
    {
        /// <summary>
        ///     This value should be the name of your universe
        /// </summary>
        public String Name { get; set; } = "Universe";

        /// <summary>
        ///     This value should be an integer X for your width
        /// </summary>
        public Int32 GridX { get; set; } = 8;

        /// <summary>
        ///     This value should be an integer Y for your height
        /// </summary>
        public Int32 GridY { get; set; } = 10;
    }
}