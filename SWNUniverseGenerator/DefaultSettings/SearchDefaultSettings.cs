using System;

namespace SWNUniverseGenerator.DefaultSettings
{
    /// <summary>
    /// This class holds the parameters needed when creating a search query
    /// </summary>
    public class SearchDefaultSettings
    {
        /// <summary>
        /// A list of String IDs that will be used in a search
        /// </summary>
        public String[] ID { get; set; }
        
        /// <summary>
        /// A list of String Names that will be used in a search
        /// </summary>
        public String[] Name { get; set; }
        
        /// <summary>
        /// The Index of the item being retrieved from the search
        /// </summary>
        public Int32 Index { get; set; }
        
        /// <summary>
        /// A list of Tags that can be used to further limit a search
        /// </summary>
        public String[] Tag { get; set; }
    }
}