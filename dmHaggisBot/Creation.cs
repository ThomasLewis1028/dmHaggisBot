using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Newtonsoft.Json;

namespace dmHaggisBot
{
    public class Creation
    {
        private static readonly string cwd = @"C:\Users\Thomas Lewis\RiderProjects\dmHaggisBot\dmHaggisBot\";
        private static readonly string univD = cwd + "\\UniverseFiles\\";
        
        public Universe CreateUniverse(UniverseDefaultSettings universeDefaultSettings)
        {
            Directory.SetCurrentDirectory(univD);
            StringBuilder path = new StringBuilder();
            path.Append(Directory.GetCurrentDirectory() + "\\" + universeDefaultSettings.Name + ".json");
            
            if (File.Exists(path.ToString()))
            {
                if (universeDefaultSettings.Overwrite.ToUpper() == "Y")
                    File.Delete(path.ToString());
                else
                    throw new FileLoadException(String.Format("{0} already exists. Use -o to overwrite or use loadUni", universeDefaultSettings.Name));
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

            return new Universe(name, grid);
        }


        public Universe CreateStars(Universe universe, StarDefaultSettings starDefaultSettings)
        {
            return universe;
        }

        public Universe CreateCharacter(Universe universe, CharacterDefaultSettings characterDefaultSettings)
        {
            return new CharCreation().AddCharacter(universe, characterDefaultSettings);
        }

        public Universe SerializeData(Universe universe, string path)
        {
            universe.Characters = universe.Characters.OrderBy(c => c.First).ToList();

            using StreamWriter file =
                File.CreateText(path.ToString());
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, universe);

            return universe;
        }
    }
}