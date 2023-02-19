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
            Grid = new Grid(8, 10);
            Overwrite = false;
        }

        /// <summary>
        ///     This value should be the name of your universe
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        ///     This value should be a string x y that will be your X Y grid
        /// </summary>
        public Grid Grid { get; set; }

        /// <summary>
        ///     Flag for overwriting a previous universe file
        ///     Y for yes, N for no
        /// </summary>
        public Boolean Overwrite { get; set; }
    }
}