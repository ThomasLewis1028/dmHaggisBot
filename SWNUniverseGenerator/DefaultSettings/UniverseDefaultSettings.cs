using System;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.DefaultSettings
{
    /// <summary>
    ///     This is the default creator class for Universe Objects
    /// </summary>
    public class UniverseDefaultSettings
    {
        public UniverseDefaultSettings()
        {
            Name = null;
            GridX = 8;
            GridY = 10;
            Overwrite = false;
        }

        /// <summary>
        ///     This value should be the name of your universe
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        ///     This value should be an integer X for your width
        /// </summary>
        public Int32? GridX { get; set; }
        
        /// <summary>
        ///     This value should be an integer Y for your height
        /// </summary>
        public Int32? GridY { get; set; }

        /// <summary>
        ///     Flag for overwriting a previous universe file
        ///     Y for yes, N for no
        /// </summary>
        public Boolean Overwrite { get; set; }
    }
}