using System.Collections.Generic;

namespace dmHaggisBot
{
    public class Stars
    {
        private List<Star> starList = new List<Star>();

        public List<Star> StarList
        {
            get => starList;
            set => starList = value;
        }
    }
}