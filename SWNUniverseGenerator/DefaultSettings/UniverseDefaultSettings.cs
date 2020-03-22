using System;

namespace SWNUniverseGenerator
{
    /// <summary>
    ///     This is the default creator class for Universe Objects
    /// </summary>
    public class UniverseDefaultSettings
    {
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
        public String Overwrite { get; set; }
    }
}