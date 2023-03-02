using System.Collections.Generic;

namespace SWNUniverseGenerator.DeserializedObjects
{
    /// <summary>
    /// This is the class where a worldTags.json will be deserialized to
    /// </summary>
    public class WorldInfo
    {
        /// <summary>
        /// This stores a list of Origins for Planets to be randomly pulled from
        /// </summary>
        public List<string> OwOrigins { get; set; }

        /// <summary>
        /// This stores a list of Relationships between a Planet and the Primary Planet
        /// To be randomly pulled from
        /// </summary>
        public List<string> OwRelationships { get; set; }

        /// <summary>
        /// This stores a list of Contact reasons between a Planet and the Primary Planet
        /// To be randomly pulled from
        /// </summary>
        public List<string> OwContacts { get; set; }
    }
}