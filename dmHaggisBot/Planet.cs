namespace dmHaggisBot
{
    public class Planet
    {
        private string _name;
        private WorldTag _worldTag;
        private Temperature _temperature;
        private Atmosphere _atmosphere;
        private Biosphere _biosphere;
        private Population _population;
        private TechLevel _techLevel;
        private string _origin;
        private string _relationship;
        private string _contact;

        public Planet(string name)
        {
            this._name = name;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public WorldTag WorldTag
        {
            get => _worldTag;
            set => _worldTag = value;
        }

        public Atmosphere Atmosphere
        {
            get => _atmosphere;
            set => _atmosphere = value;
        }

        public Temperature Temperature
        {
            get => _temperature;
            set => _temperature = value;
        }

        public Biosphere Biosphere
        {
            get => _biosphere;
            set => _biosphere = value;
        }

        public Population Population
        {
            get => _population;
            set => _population = value;
        }

        public TechLevel TechLevel
        {
            get => _techLevel;
            set => _techLevel = value;
        }

        public string Origin
        {
            get => _origin;
            set => _origin = value;
        }

        public string Relationship
        {
            get => _relationship;
            set => _relationship = value;
        }

        public string Contact
        {
            get => _contact;
            set => _contact = value;
        }
    }
}