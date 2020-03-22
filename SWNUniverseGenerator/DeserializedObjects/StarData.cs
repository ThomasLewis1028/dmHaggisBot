using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator
{
    /// <summary>
    /// Hold all data of Star and Planet names to be used in generation
    /// </summary>
    internal class StarData
    {
        // Hold a list of Star Names for generation
        public List<String> Stars { get; set; }
        
        // Hold a list of Planet Names for generation
        public List<String> Planets { get; set; }
    }
}