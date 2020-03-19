namespace SWNUniverseGenerator
{
    public class City
    {
        public City(string name, Planet planet)
        {
            name = name;
            Planet = planet;
        }

        public string Name { get; set; }

        public Planet Planet { get; set; }

        public int Pop { get; set; }
    }
}