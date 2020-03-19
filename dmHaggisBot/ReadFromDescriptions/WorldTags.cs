using System.Collections.Generic;

namespace dmHaggisBot.ReadFromDescripstions
{
    public class WorldTagList
    {
        private List<WorldTag> _worldTags = new List<WorldTag>();

        public List<WorldTag> WorldTags
        {
            get => _worldTags;
            set => _worldTags = value;
        }
    }
}