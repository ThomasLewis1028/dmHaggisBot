using System;

namespace SWNUniverseGenerator
{
    public class SearchResult
    {
        public IEntity Result { get; set; }
        
        public Int32 CurrentCount { get; set; }
        
        public Int32 MaxCount { get; set; }
        
        public SearchResult(IEntity result, int currentCount, int maxCount)
        {
            Result = result;
            CurrentCount = currentCount;
            MaxCount = maxCount;
        }
    }
}