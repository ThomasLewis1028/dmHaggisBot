using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator
{
    public class Search
    {
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
            var id = searchDefaultSettings.ID ?? new string[] { };
            var n = searchDefaultSettings.Name ?? new string[] { };
            var t = searchDefaultSettings.Tag ?? new string[] { };
            var cl = searchDefaultSettings.CurrentLocation ?? new string[] { };

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
            var clrgx = "";
            if (cl.Length != 0)
            {
                foreach (var i in cl)
                    clrgx += "" + i + "|";
                clrgx = clrgx.Substring(0, clrgx.Length - 1);
            }
            else
                clrgx = "^$";

            // Set the tags for more specific searches
            bool includePlanets = t.Contains("p") || t.Length == 0;
            bool includeChars = t.Contains("ch") || t.Length == 0;
            bool includeStars = t.Contains("s") || t.Length == 0;
            bool includeProbs = (t.Contains("pr") || t.Length == 0)
                                && searchDefaultSettings.Permission != SearchDefaultSettings.PermissionType.Player;

            return (from p in universe.Planets
                    where (Regex.IsMatch(p.Name, nrgx, RegexOptions.IgnoreCase) ||
                           Regex.IsMatch(p.ID, idrgx, RegexOptions.IgnoreCase)) &&
                          includePlanets
                    select (IEntity) p)
                .Union(from ch in universe.Characters
                    where (Regex.IsMatch(ch.Name, nrgx, RegexOptions.IgnoreCase) ||
                           Regex.IsMatch(ch.ID, idrgx, RegexOptions.IgnoreCase) ||
                           Regex.IsMatch(ch.CurrentLocation, clrgx, RegexOptions.IgnoreCase)) &&
                          includeChars
                    select (IEntity) ch)
                .Union(from s in universe.Stars
                    where (Regex.IsMatch(s.Name, nrgx, RegexOptions.IgnoreCase) ||
                           Regex.IsMatch(s.ID, idrgx, RegexOptions.IgnoreCase)) &&
                          includeStars
                    select (IEntity) s)
                .Union(from pr in universe.Problems
                    where (Regex.IsMatch(pr.ID, idrgx, RegexOptions.IgnoreCase) &&
                           includeProbs)
                    select (IEntity) pr)
                .AsQueryable();
        }
    }
}