using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator.DeserializedObjects
{
    /// <summary>
    /// This is where a characterData.json file is deserialized to
    /// </summary>
    public class CharData
    {
        /// <summary>
        /// Holds the String List of Male First names
        /// </summary>
        public List<String> MaleName { get; set; }
        
        /// <summary>
        /// Holds the String List of Female First names 
        /// </summary>
        public List<String> FemaleName { get; set; }
        
        /// <summary>
        /// Holds the String List of all Last Names
        /// </summary>
        public List<String> LastName { get; set; }
        
        /// <summary>
        /// Holds the String List of HairColors
        /// </summary>
        public List<String> HairColor { get; set; }
        
        /// <summary>
        /// Holds the String List of HairStyles
        /// </summary>
        public List<String> HairStyle { get; set; }
        
        /// <summary>
        /// Holds the String List of EyeColors
        /// </summary>
        public List<String> EyeColor { get; set; }
    }
}