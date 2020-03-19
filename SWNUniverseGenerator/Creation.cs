using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SWNUniverseGenerator
{
    public class Creation
    {
        private readonly string universePath;

        public Creation(string path)
        {
            universePath = path;
        }

        public Universe CreateUniverse(UniverseDefaultSettings universeDefaultSettings)
        {
            var path = new StringBuilder();
            path.Append(universePath + "\\" + universeDefaultSettings.Name + ".json");

            if (File.Exists(path.ToString()))
            {
                if (universeDefaultSettings.Overwrite.ToUpper() == "Y")
                    File.Delete(path.ToString());
                else
                    throw new IOException(string.Format("{0} already exists. Use -o to overwrite or use loadUni",
                        universeDefaultSettings.Name));
            }

            File.Create(path.ToString()).Close();

            var name = string.IsNullOrEmpty(universeDefaultSettings.Name)
                ? "Universe"
                : universeDefaultSettings.Name;

            Grid grid;
            if (string.IsNullOrEmpty(universeDefaultSettings.Grid))
                grid = new Grid(8, 10);
            else
            {
                var g = universeDefaultSettings.Grid.Split(" ");
                grid = new Grid(int.Parse(g[0]), int.Parse(g[1]));
            }

            var universe = new Universe(name, grid);
            SerializeData(universe);
            return new Universe(name, grid);
        }


        public Universe CreateStars(Universe universe, StarDefaultSettings starDefaultSettings)
        {
            universe = new StarCreation().AddStars(universe, starDefaultSettings);
            SerializeData(universe);
            return universe;
        }

        public Universe CreateCharacter(Universe universe, CharacterDefaultSettings characterDefaultSettings)
        {
            universe = new CharCreation().AddCharacter(universe, characterDefaultSettings);
            SerializeData(universe);
            return universe;
        }

        public Universe LoadUniverse(string name)
        {
            var path = new StringBuilder();
            path.Append(universePath + "\\" + name + ".json");

            if (!File.Exists(path.ToString()))
                throw new FileNotFoundException("{0}.json not found.");

            var univ =
                JObject.Parse(
                    File.ReadAllText(path.ToString()));

            return JsonConvert.DeserializeObject<Universe>(univ.ToString());
        }

        public Universe SerializeData(Universe universe)
        {
            var path = universePath + "\\" + universe.Name + ".json";
            using var file =
                File.CreateText(path);
            var serializer = new JsonSerializer();
            serializer.Serialize(file, universe);

            return universe;
        }
    }
}