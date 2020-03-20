using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            if (universeDefaultSettings.Grid == null)
                grid = new Grid(8, 10);
            else
            {
                grid = universeDefaultSettings.Grid;
            }

            var universe = new Universe(name, grid);
            SerializeData(universe);
            return new Universe(name, grid);
        }

        public Universe CreateStars(Universe universe, StarDefaultSettings starDefaultSettings)
        {
            if (universe.Grid == null)
                throw new FileNotFoundException("No grid has been set for the universe");
            
            universe = new StarCreation().AddStars(universe, starDefaultSettings);
            SerializeData(universe);
            return universe;
        }
        
        public Universe CreatePlanets(Universe universe, PlanetDefaultSettings planetDefaultSettings)
        {
            if (universe.Stars == null || universe.Stars.Count == 0)
                throw new FileNotFoundException("No stars have been created for the universe");

            universe = new PlanetCreation().AddPlanets(universe, planetDefaultSettings);
            SerializeData(universe);
            return universe;
        }

        public Universe CreateCharacter(Universe universe, CharacterDefaultSettings characterDefaultSettings)
        {
            if(universe.Planets == null || universe.Planets.Count == 0)
                throw new FileNotFoundException("No planets have been created for the universe");
            
            universe = new CharCreation().AddCharacter(universe, characterDefaultSettings);
            SerializeData(universe);
            return universe;
        }

        public SearchResult SearchUniverse(Universe universe, SearchDefaultSettings searchDefaultSettings)
        {
            var id = string.IsNullOrEmpty(searchDefaultSettings.ID)
                ? null
                : searchDefaultSettings.ID.Split(", ");

            var n = string.IsNullOrEmpty(searchDefaultSettings.Name)
                ? null
                : searchDefaultSettings.Name.Split(", ");

            var c = searchDefaultSettings.Count == 0
                ? 0
                : searchDefaultSettings.Count - 1;

            var t = string.IsNullOrEmpty(searchDefaultSettings.Tag)
                ? null
                : searchDefaultSettings.Tag;

            var results = new List<IEntity>();

            var planets = new List<Planet>();
            var stars = new List<Star>();
            var characters = new List<Character>();

            if (id != null)
            {
                planets = planets.Concat(universe.Planets.FindAll(a => id.Contains(a.ID))).ToList();
                stars = stars.Concat(universe.Stars.FindAll(a => id.Contains(a.ID))).ToList();
                characters = characters.Concat(universe.Characters.FindAll(a => id.Contains(a.ID))).ToList();
            }

            if (n != null)
            {
                planets = planets.Concat(universe.Planets.FindAll(a => n.Contains(a.Name))).ToList();
                stars = stars.Concat(universe.Stars.FindAll(a => n.Contains(a.Name))).ToList();
                characters = characters.Concat(universe.Characters.FindAll(a =>
                    n.Contains(a.First) || n.Contains(a.Last) || n.Contains(a.Name))).ToList();
            }

            results = string.IsNullOrEmpty(t) || t == "p"
                ? results.Concat(planets.ToList<IEntity>()).ToList()
                : results;
            results = string.IsNullOrEmpty(t) || t == "s"
                ? results.Concat(stars.ToList<IEntity>()).ToList()
                : results;
            results = string.IsNullOrEmpty(t) || t == "ch"
                ? results.Concat(characters.ToList<IEntity>()).ToList()
                : results;

            if (results.Count == 0 || results.Count <= c || results.Count < 0)
            {
                return new SearchResult(null, 0, 0);
            }
            
            return new SearchResult(results[c], c + 1, results.Count);
            
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

        public void SerializeData(Universe universe)
        {
            var path = universePath + "\\" + universe.Name + ".json";
            using var file =
                File.CreateText(path);
            var serializer = new JsonSerializer();
            serializer.Serialize(file, universe);
        }
    }
}