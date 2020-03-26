using System.Collections.Generic;

namespace SWNUniverseGenerator.DeserializedObjects
{
    /// <summary>
    /// This is the class where a worldTags.json will be deserialized to
    /// </summary>
    public class WorldInfo
    {
        /// <summary>
        /// This stores a list of Atmospheres to be randomly pulled from
        /// </summary>
        public List<Atmosphere> Atmospheres { get; set; }

        /// <summary>
        /// This stores a list of WorldTags to be randomly pulled from
        /// </summary>
        public List<WorldTag> WorldTags { get; set; }

        /// <summary>
        /// This stores a list of Temperatures to be randomly pulled from
        /// </summary>
        public List<Temperature> Temperatures { get; set; }

        /// <summary>
        /// This stores a list of Biospheres to be randomly pulled from
        /// </summary>
        public List<Biosphere> Biospheres { get; set; }

        /// <summary>
        /// This stores a list of Populations to be randomly pulled from
        /// </summary>
        public List<Population> Populations { get; set; }

        /// <summary>
        /// This stores a list of TechLevels to be randomly pulled from
        /// </summary>
        public List<TechLevel> TechLevels { get; set; }

        /// <summary>
        /// This stores a list of Origins for Planets to be randomly pulled from
        /// </summary>
        public List<string> OWOrigins { get; set; }

        /// <summary>
        /// This stores a list of Relationships between a Planet and the Primary Planet
        /// To be randomly pulled from
        /// </summary>
        public List<string> OWRelationships { get; set; }

        /// <summary>
        /// This stores a list of Contact reasons between a Planet and the Primary Planet
        /// To be randomly pulled from
        /// </summary>
        public List<string> OWContacts { get; set; }
    }

    /// <summary>
    /// This is where the World Tags from Stars Without Number's rulebook are deserialized to
    /// </summary>
    public class WorldTag
    {
        /// <summary>
        /// Stores the type of World Tag
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Stores the Description of the World Tag
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Stores a list of potential Enemies
        /// </summary>
        public string[] Enemies { get; set; }

        /// <summary>
        /// Stores a list of potential Friends
        /// </summary>
        public string[] Friends { get; set; }

        /// <summary>
        /// Stores a list of potential Complications
        /// </summary>
        public string[] Complications { get; set; }

        /// <summary>
        /// Stores a list of potential Things to find
        /// </summary>
        public string[] Things { get; set; }

        /// <summary>
        /// Stores a list of potential Places
        /// </summary>
        public string[] Places { get; set; }
    }

    /// <summary>
    /// This holds information about an Atmosphere
    /// </summary>
    public class Atmosphere
    {
        /// <summary>
        /// Stores the type of Atmosphere
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Stores the Description of the Atmosphere
        /// </summary>
        public string Description { get; set; }
    }

    
    /// <summary>
    /// This holds information about a Temperature
    /// </summary>
    public class Temperature
    {
        /// <summary>
        /// Stores the type of Temperature
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Stores the Description of the Temperature
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// This hold information about a Biosphere
    /// </summary>
    public class Biosphere
    {
        /// <summary>
        /// Stores the type of Biosphere
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// Stores the Description of the Biosphere
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// This holds information about a Population
    /// </summary>
    public class Population
    {
        /// <summary>
        /// Stores the type of Population
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// Stores the Description of the Population
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// This holds information about a Tech Level
    /// </summary>
    public class TechLevel
    {
        /// <summary>
        /// Stores the type of Tech Level
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Stores the Short Description of the Tech Level
        /// </summary>
        public string ShortDesc { get; set; }

        /// <summary>
        /// Stores the Long Description of the Tech Level
        /// </summary>
        public string Description { get; set; }
    }
}