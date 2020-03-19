using System.Collections.Generic;

namespace SWNUniverseGenerator
{
    internal class WorldInfo
    {
        public List<Atmosphere> Atmospheres { get; set; }

        public List<WorldTag> WorldTags { get; set; }

        public List<Temperature> Temperatures { get; set; }

        public List<Biosphere> Biospheres { get; set; }

        public List<Population> Populations { get; set; }

        public List<TechLevel> TechLevels { get; set; }

        public List<string> OWOrigins { get; set; }

        public List<string> OWRelationships { get; set; }

        public List<string> OWContacts { get; set; }
    }

    public class WorldTag
    {
        public string Type { get; set; }

        public string Description { get; set; }

        public string[] Enemies { get; set; }

        public string[] Friends { get; set; }

        public string[] Complications { get; set; }

        public string[] Things { get; set; }

        public string[] Places { get; set; }
    }

    public class Atmosphere
    {
        public string Type { get; set; }

        public string Description { get; set; }
    }

    public class Temperature
    {
        public string Type { get; set; }

        public string Description { get; set; }
    }

    public class Biosphere
    {
        public string Type { get; set; }

        public string Description { get; set; }
    }

    public class Population
    {
        public string Type { get; set; }

        public string Description { get; set; }
    }

    public class TechLevel
    {
        public string Type { get; set; }

        public string ShortDesc { get; set; }

        public string Description { get; set; }
    }
}