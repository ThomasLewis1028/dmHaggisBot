﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator
{
    public class Search
    {
        /// <summary>
        /// This is the Search function that will iterate through all of the created objects and returns the specified
        /// index. It receives a Universe to search through and a SearchDefaultSettings to apply the settings.
        ///
        /// Example: if a search yields 5 results and you ask for the index of 3, it will return the third item
        /// in the list. Going above or below the range of the search results will return "No results found".
        /// </summary>
        /// <param name="universe"></param>
        /// <param name="searchDefaultSettings"></param>
        /// <returns>
        /// Return the single SearchResult.
        /// </returns>
        public static SearchResult SearchUniverse(Universe universe, SearchDefaultSettings searchDefaultSettings)
        {
            // Set the count of what result number is requested. Default to 0
            var c = searchDefaultSettings.Index == 0
                ? 0
                : searchDefaultSettings.Index - 1;

            var maxCount = GetSearchQuery(universe, searchDefaultSettings).Count();

            // Use a Linq query to find all of the IDs in the list that match your search query and take the specified index
            var item = GetSearchQuery(universe, searchDefaultSettings)
                .Skip(c)
                .Take(1)
                .SingleOrDefault();

            // If there were no results then return null
            if (maxCount == 0 || maxCount <= c || maxCount < 0 || item == null)
                return new SearchResult(null, 0, 0);

            return new SearchResult(item, c + 1, maxCount);
        }

        /// <summary>
        /// This is the Search function that will iterate through all of the created objects and returns all results.
        /// It receives a Universe to search through and a SearchDefaultSettings to apply the settings.
        /// </summary>
        /// <param name="universe"></param>
        /// <param name="searchDefaultSettings"></param>
        /// <returns>
        /// Return the entire set of results
        /// </returns>
        public static List<IEntity> SearchUniverseList(Universe universe, SearchDefaultSettings searchDefaultSettings)
        {
            // Use an IQueryable to retrieve all of te items that match the search result
            var query = GetSearchQuery(universe, searchDefaultSettings);

            return query.ToList();
        }

        /// <summary>
        /// This is the query to search through all sets of data in the Universe
        /// </summary>
        /// <param name="universe"></param>
        /// <param name="searchDefaultSettings"></param>
        /// <returns>
        /// IQueryable of type IEntity
        /// </returns>
        private static IQueryable<IEntity> GetSearchQuery(Universe universe,
            SearchDefaultSettings searchDefaultSettings)
        {
            // Check if the ID or Name tags have values and set those. Default to empty arrays.
            var id = searchDefaultSettings.Id ?? new string[] { };
            var n = searchDefaultSettings.Name ?? new string[] { };
            var t = searchDefaultSettings.Tag ?? new string[] { };
            var l = searchDefaultSettings.Location ?? new string[] { };

            // Set a regular expression for the Name
            var nrgx = "";
            if (n.Length != 0)
            {
                foreach (var i in n)
                    nrgx += "" + i + "|";
                nrgx = nrgx.Substring(0, nrgx.Length - 1);
            }
            else
                nrgx = "^$";

            // Set a regular expression for the ID
            var idrgx = "";
            if (id.Length != 0)
            {
                foreach (var i in id)
                    idrgx += "" + i + "|";
                idrgx = idrgx.Substring(0, idrgx.Length - 1);
            }
            else
                idrgx = "^$";

            // Set a regular expression for the ID
            var lrgx = "";
            if (l.Length != 0)
            {
                foreach (var i in l)
                    lrgx += "" + i + "|";
                lrgx = lrgx.Substring(0, lrgx.Length - 1);
            }
            else
                lrgx = "^$";

            // Set the tags for more specific searches
            var includePlanets = t.Contains("p") || t.Length == 0;
            var includeChars = t.Contains("ch") || t.Length == 0;
            var includeStars = t.Contains("s") || t.Length == 0;
            var includeProbs = (t.Contains("pr") || t.Length == 0)
                               && searchDefaultSettings.Permission != SearchDefaultSettings.PermissionType.Player;
            var includePoi = t.Contains("poi") || t.Length == 0;
            var includeZones = t.Contains("z") || t.Length == 0;
            var includeShips = t.Contains("sh") || t.Length == 0;
            var includeAliens = t.Contains("a") || t.Length == 0;

            return (from p in universe.Planets
                    where (Regex.IsMatch(p.Name, nrgx, RegexOptions.IgnoreCase) ||
                           Regex.IsMatch(p.Id, idrgx, RegexOptions.IgnoreCase)) &&
                          includePlanets
                    select (IEntity) p)
                .Union(from ch in universe.Characters
                    where (Regex.IsMatch(ch.Name, nrgx, RegexOptions.IgnoreCase) ||
                           Regex.IsMatch(ch.Id, idrgx, RegexOptions.IgnoreCase) ||
                           Regex.IsMatch(ch.CurrentLocation, lrgx, RegexOptions.IgnoreCase)) &&
                          includeChars
                    select (IEntity) ch)
                .Union(from s in universe.Stars
                    where (Regex.IsMatch(s.Name, nrgx, RegexOptions.IgnoreCase) ||
                           Regex.IsMatch(s.Id, idrgx, RegexOptions.IgnoreCase)) &&
                          includeStars
                    select (IEntity) s)
                .Union(from pr in universe.Problems
                    where Regex.IsMatch(pr.Id, idrgx, RegexOptions.IgnoreCase) &&
                          includeProbs
                    select (IEntity) pr)
                .Union(from poi in universe.PointsOfInterest
                    where (Regex.IsMatch(poi.Id, idrgx, RegexOptions.IgnoreCase) ||
                           Regex.IsMatch(poi.StarId, lrgx, RegexOptions.IgnoreCase)) &&
                          includePoi
                    select (IEntity) poi)
                .Union(from ship in universe.Ships
                    where Regex.IsMatch(ship.Id, idrgx, RegexOptions.IgnoreCase) &&
                          includeShips
                    select (IEntity) ship)
                .Union(from alien in universe.Aliens
                    where Regex.IsMatch(alien.Id, idrgx, RegexOptions.IgnoreCase) && includeAliens
                    select (IEntity) alien)
                .Union(from z in universe.Zones
                    where Regex.IsMatch(z.Id, idrgx, RegexOptions.IgnoreCase) ||
                          Regex.IsMatch(z.Name, nrgx, RegexOptions.IgnoreCase) &&
                          includeZones
                    select (IEntity) z)
                .AsQueryable();
        }
    }
}