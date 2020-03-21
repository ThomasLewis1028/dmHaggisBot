using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SWNUniverseGenerator
{
    public class Creation
    {
        private readonly string _universePath;

        public Creation(string path)
        {
            _universePath = path;
        }

        public Universe CreateUniverse(UniverseDefaultSettings universeDefaultSettings)
        {
            var path = new StringBuilder();
            path.Append(_universePath + "\\" + universeDefaultSettings.Name + ".json");

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
            if (universe.Planets == null || universe.Planets.Count == 0)
                throw new FileNotFoundException("No planets have been created for the universe");

            universe = new CharCreation().AddCharacter(universe, characterDefaultSettings);
            SerializeData(universe);
            return universe;
        }

        public Universe CreateProblems(Universe universe, ProblemDefaultSettings problemDefaultSettings)
        {
            if (universe.Planets == null || universe.Planets.Count == 0)
                throw new FileNotFoundException("No locations have been loaded.");

            universe = new ProblemCreation().AddProblems(universe, problemDefaultSettings);
            SerializeData(universe);
            return universe;
        }

        public SearchResult SearchUniverse(Universe universe, SearchDefaultSettings searchDefaultSettings)
        {
            var id = string.IsNullOrEmpty(searchDefaultSettings.ID)
                ? new string[] { }
                : searchDefaultSettings.ID.Split(", ");

            var n = string.IsNullOrEmpty(searchDefaultSettings.Name)
                ? new string[] { }
                : searchDefaultSettings.Name.Split(", ");

            var nrgx = "(";

            foreach (var i in n)
            {
                nrgx += "" + i + "|";
            }

            nrgx = nrgx.Substring(0, nrgx.Length - 1) + ")";

            var c = searchDefaultSettings.Count == 0
                ? 0
                : searchDefaultSettings.Count - 1;

            var t = string.IsNullOrEmpty(searchDefaultSettings.Tag)
                ? null
                : searchDefaultSettings.Tag;

            IEntity result = null;
            bool includePlanets = string.IsNullOrEmpty(t) || t.Contains("p");
            bool includeChars = string.IsNullOrEmpty(t) || t.Contains("ch");
            bool includeStars = string.IsNullOrEmpty(t) || t.Contains("s");

            var maxCount = (from p in universe.Planets
                        where (Regex.IsMatch(p.Name, nrgx, RegexOptions.IgnoreCase) || id.Contains(p.ID)) &&
                              includePlanets
                        select new {p.ID, p.Name})
                    .Union(from ch in universe.Characters
                        where (Regex.IsMatch(ch.Name, nrgx, RegexOptions.IgnoreCase) || id.Contains(ch.ID)) &&
                              includeChars
                        select new {ch.ID, ch.Name})
                    .Union(from s in universe.Stars
                        where (Regex.IsMatch(s.Name, nrgx, RegexOptions.IgnoreCase) || id.Contains(s.ID)) &&
                              includeStars
                        select new {s.ID, s.Name})
                    .Count()
                ;

            var item = (from p in universe.Planets
                    where (Regex.IsMatch(p.Name, nrgx, RegexOptions.IgnoreCase) || id.Contains(p.ID)) && includePlanets
                    select new {p.ID, p.Name, Type = SearchType.Planets})
                .Union(from ch in universe.Characters
                    where (Regex.IsMatch(ch.Name, nrgx, RegexOptions.IgnoreCase) || id.Contains(ch.ID)) && includeChars
                    select new {ch.ID, ch.Name, Type = SearchType.Characters})
                .Union(from s in universe.Stars
                    where (Regex.IsMatch(s.Name, nrgx, RegexOptions.IgnoreCase) || id.Contains(s.ID)) && includeStars
                    select new {s.ID, s.Name, Type = SearchType.Stars})
                .Skip(c)
                .Take(1)
                .SingleOrDefault();

            if (item != null)
            {
                switch (item.Type)
                {
                    case SearchType.Characters:
                        result = universe.Characters.Single(a => a.ID == item.ID);
                        break;
                    case SearchType.Planets:
                        result = universe.Planets.Single(a => a.ID == item.ID);
                        break;
                    case SearchType.Stars:
                        result = universe.Stars.Single(a => a.ID == item.ID);
                        break;
                }
            }

            if (maxCount == 0 || maxCount <= c || maxCount < 0 || result == null)
            {
                return new SearchResult(null, 0, 0);
            }

            return new SearchResult(result, c + 1, maxCount);
        }

        public Universe LoadUniverse(string name)
        {
            var path = new StringBuilder();
            path.Append(_universePath + "\\" + name + ".json");

            if (!File.Exists(path.ToString()))
                throw new FileNotFoundException("{0}.json not found.");

            var univ =
                JObject.Parse(
                    File.ReadAllText(path.ToString()));

            return JsonConvert.DeserializeObject<Universe>(univ.ToString());
        }

        public void SerializeData(Universe universe)
        {
            var path = _universePath + "\\" + universe.Name + ".json";
            using var file =
                File.CreateText(path);
            var serializer = new JsonSerializer();
            serializer.Serialize(file, universe);
        }
    }

    public enum SearchType
    {
        Planets,
        Stars,
        Characters
    }
}