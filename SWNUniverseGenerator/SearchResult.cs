using System;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator
{
    /// <summary>
    /// A Search Result that yields the specified object, the current Index, and the MaxCount of the results
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// All SearchResults must have a Result, CurrentCount and MaxCount and must be instantiated to use
        /// </summary>
        /// <param name="result"></param>
        /// <param name="currentIndex"></param>
        /// <param name="maxCount"></param>
        public SearchResult(IEntity result, int currentIndex, int maxCount)
        {
            Result = result;
            CurrentIndex = currentIndex;
            MaxCount = maxCount;
        }

        /// <summary>
        /// The actual Result from the search
        /// This can be any object that implements IEntity
        /// </summary>
        public IEntity Result { get; set; }

        /// <summary>
        /// The Current Index from the search query
        /// </summary>
        public Int32 CurrentIndex { get; set; }

        
        /// <summary>
        /// The maximum number that a given search query can yield
        /// </summary>
        public Int32 MaxCount { get; set; }
    }
}