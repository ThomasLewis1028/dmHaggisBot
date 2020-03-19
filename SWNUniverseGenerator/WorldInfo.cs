using System.Collections.Generic;
using Newtonsoft.Json;

namespace SWNUniverseGenerator
{
    public class WorldInfo
    {
        private List<WorldTag> _worldTags;
        private List<Atmosphere> _atmospheres;
        private List<Temperature> _temperatures;
        private List<Biosphere> _biospheres;
        private List<Population> _populations;
        private List<TechLevel> _techLevels;
        private List<string> _owOrigins;
        private List<string> _owRelationships;
        private List<string> _owContacts;

        public List<Atmosphere> Atmospheres
        {
            get => _atmospheres;
            set => _atmospheres = value;
        }

        public List<WorldTag> WorldTags
        {
            get => _worldTags;
            set => _worldTags = value;
        }

        public List<Temperature> Temperatures
        {
            get => _temperatures;
            set => _temperatures = value;
        }

        public List<Biosphere> Biospheres
        {
            get => _biospheres;
            set => _biospheres = value;
        }

        public List<Population> Populations
        {
            get => _populations;
            set => _populations = value;
        }

        public List<TechLevel> TechLevels
        {
            get => _techLevels;
            set => _techLevels = value;
        }

        public List<string> OWOrigins
        {
            get => _owOrigins;
            set => _owOrigins = value;
        }

        public List<string> OWRelationships
        {
            get => _owRelationships;
            set => _owRelationships = value;
        }

        public List<string> OWContacts
        {
            get => _owContacts;
            set => _owContacts = value;
        }
    }

    public class WorldTag
    {
        private string _type;
        private string _description;
        private string[] _enemies;
        private string[] _friends;
        private string[] _complications;
        private string[] _things;
        private string[] _places;

        public string Type
        {
            get => _type;
            set => _type = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public string[] Enemies
        {
            get => _enemies;
            set => _enemies = value;
        }

        public string[] Friends
        {
            get => _friends;
            set => _friends = value;
        }

        public string[] Complications
        {
            get => _complications;
            set => _complications = value;
        }

        public string[] Things
        {
            get => _things;
            set => _things = value;
        }

        public string[] Places
        {
            get => _places;
            set => _places = value;
        }
    }

    public class Atmosphere
    {
        private string _type;
        private string _description;

        public string Type
        {
            get => _type;
            set => _type = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }
    }

    public class Temperature
    {
        private string _type;
        private string _description;

        public string Type
        {
            get => _type;
            set => _type = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }
    }

    public class Biosphere
    {
        private string _type;
        private string _description;

        public string Type
        {
            get => _type;
            set => _type = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }
    }

    public class Population
    {
        private string _type;
        private string _description;

        public string Type
        {
            get => _type;
            set => _type = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }
    }

    public class TechLevel
    {
        private string _type;
        private string _shortDesc;
        private string _description;

        public string Type
        {
            get => _type;
            set => _type = value;
        }

        public string ShortDesc
        {
            get => _shortDesc;
            set => _shortDesc = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }
    }
}