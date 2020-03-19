using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SWNUniverseGenerator
{
    public class Creation
    {
        private static readonly string cwd = @"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\dmHaggisBot\";
        private static readonly string univD = cwd + "\\UniverseFiles\\";

        public Universe CreateUniverse(UniverseDefaultSettings universeDefaultSettings)
        {
            StringBuilder path = new StringBuilder();
            path.Append(univD + "\\" + universeDefaultSettings.Name + ".json");

            if (File.Exists(path.ToString()))
            {
                if (universeDefaultSettings.Overwrite.ToUpper() == "Y")
                    File.Delete(path.ToString());
                else
                    throw new IOException(String.Format("{0} already exists. Use -o to overwrite or use loadUni",
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
                grid = new Grid(Int32.Parse(g[0]), Int32.Parse(g[1]));
            }

            Universe universe = new Universe(name, grid);
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
            StringBuilder path = new StringBuilder();
            path.Append(univD + "\\" + name + ".json");
            
            if (!File.Exists(path.ToString()))
                throw new FileNotFoundException("{0}.json not found.");

            JObject univ =
                JObject.Parse(
                    File.ReadAllText(path.ToString()));

            return JsonConvert.DeserializeObject<Universe>(univ.ToString());
        }

        public Universe SerializeData(Universe universe)
        {
            var path = univD + "\\" + universe.Name + ".json";
            using StreamWriter file =
                File.CreateText(path);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, universe);

            return universe;
        }
    }
}