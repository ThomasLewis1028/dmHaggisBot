namespace SWNUniverseGenerator
{
    public class Planet
    {
        public Planet(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public WorldTag WorldTag { get; set; }

        public Atmosphere Atmosphere { get; set; }

        public Temperature Temperature { get; set; }

        public Biosphere Biosphere { get; set; }

        public Population Population { get; set; }

        public TechLevel TechLevel { get; set; }

        public string Origin { get; set; }

        public string Relationship { get; set; }

        public string Contact { get; set; }
    }
}