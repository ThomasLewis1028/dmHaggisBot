using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWNUniverseGenerator.CreationTools;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator
{
    /// <summary>
    /// This Class should be used as the entry-point for all other data
    ///
    /// Each method here is marked as Public and calls the other Creation tools as needed.
    /// Error handling is done inside the Creation tools themselves besides checking if the files exist or if the
    /// necessary "parents" have been created
    /// </summary>
    public class Creation
    {
        private readonly string _universePath;

        /// <summary>
        /// Default constructor that requires a path to be passed in
        /// </summary>
        /// <param name="path"></param>
        public Creation(string path)
        {
            _universePath = path;
        }

        /// <summary>
        /// This requires a set of UniverseDefaultSettings to create a Universe
        ///
        /// If no names or grids are set use the defaults of "Universe" and [8, 10]
        /// </summary>
        /// <param name="universeDefaultSettings"></param>
        /// <returns>The newly created Universe</returns>
        /// <exception cref="IOException"></exception>
        public Universe CreateUniverse(UniverseDefaultSettings universeDefaultSettings)
        {
            // Set the name of the Universe. Default is "Universe"
            var name = string.IsNullOrEmpty(universeDefaultSettings.Name)
                ? "Universe"
                : universeDefaultSettings.Name;

            // Set the name of the file from the name specified above
            var path = new StringBuilder();
            path.Append(_universePath + "\\" + name + ".json");

            // Look for the file based on the path above
            if (File.Exists(path.ToString()))
            {
                // If the Overwrite Tag says "Y" then overwrite the file
                if (universeDefaultSettings.Overwrite.ToUpper() == "Y")
                    File.Delete(path.ToString());
                // Otherwise throw an exception to be caught
                else
                    throw new IOException(string.Format("{0} already exists.",
                        universeDefaultSettings.Name));
            }

            // Close the file so that it can be written to by the rest of the program
            File.Create(path.ToString()).Close();

            // Set the grid to the specified values or the default [8, 10]
            Grid grid;
            if (universeDefaultSettings.Grid == null)
                grid = new Grid(8, 10);
            else
                grid = universeDefaultSettings.Grid;

            // Create, serialize, and return Universe.
            var universe = new Universe(name, grid);
            SerializeData(universe);
            return new Universe(name, grid);
        }

        /// <summary>
        /// This method should receive the Universe to add Stars to and a set of StarDefaultSettings
        ///
        /// Default values are handled in StarCreation.AddStars
        /// </summary>
        /// <param name="universe"></param>
        /// <param name="starDefaultSettings"></param>
        /// <returns>
        /// Return the newly edited Universe
        /// </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public Universe CreateStars(Universe universe, StarDefaultSettings starDefaultSettings)
        {
            // If there is no Grid for the Stars to be placed in then throw an exception
            if (universe.Grid == null)
                throw new FileNotFoundException("No grid has been set for the universe");

            // Set the Universe to the Universe returned from StarCreation.AddStars and serialize/return it
            universe = new StarCreation().AddStars(universe, starDefaultSettings);
            SerializeData(universe);
            return universe;
        }

        /// <summary>
        /// This method should receive the Universe to add Planets to and a set of PlanetDefaultSettings
        ///
        /// Default values are handled in PlanetCreation.AddPlanets
        /// </summary>
        /// <param name="universe"></param>
        /// <param name="planetDefaultSettings"></param>
        /// <returns>
        /// Return the newly edited Universe
        /// </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public Universe CreatePlanets(Universe universe, PlanetDefaultSettings planetDefaultSettings)
        {
            // If there are no Stars for the Planets to be tied to then throw an exception
            if (universe.Stars == null || universe.Stars.Count == 0)
                throw new FileNotFoundException("No stars have been created for the universe");

            // Set the Universe to the Universe returned from PlanetCreation.AddPlanets and serialize/return it
            universe = new PlanetCreation().AddPlanets(universe, planetDefaultSettings);
            SerializeData(universe);
            return universe;
        }

        /// <summary>
        /// This method should receive the Universe to add Characters to and a set of CharacterDefaultSettings
        ///
        /// Default values are handled in CharCreation.AddCharacters
        /// </summary>
        /// <param name="universe"></param>
        /// <param name="characterDefaultSettings"></param>
        /// <returns>
        /// Return the newly edited Universe
        /// </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public Universe CreateCharacter(Universe universe, CharacterDefaultSettings characterDefaultSettings)
        {
            // If there are no Planets for the Characters to be tied to then throw an exception
            if (universe.Planets == null || universe.Planets.Count == 0)
                throw new FileNotFoundException("No planets have been created for the universe");

            // Set the Universe to the Universe returned from CharCreation.AddCharacters and serialize/return it
            universe = new CharCreation().AddCharacters(universe, characterDefaultSettings);
            SerializeData(universe);
            return universe;
        }

        /// <summary>
        /// This method should receive the Universe to add Problems to and a set of ProblemDefaultSettings
        ///
        /// Default values are handled in ProblemCreation.AddProblems
        /// </summary>
        /// <param name="universe"></param>
        /// <param name="problemDefaultSettings"></param>
        /// <returns>
        /// Return the newly edited Universe
        /// </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public Universe CreateProblems(Universe universe, ProblemDefaultSettings problemDefaultSettings)
        {
            // If there are no Planets or Locations for the Problems to be tied to then throw an exception
            if (universe.Planets == null || universe.Planets.Count == 0)
                throw new FileNotFoundException("No locations have been loaded.");

            // Set the Universe to the Universe return from ProblemCreation.AddProblems and serialize/return it
            universe = new ProblemCreation().AddProblems(universe, problemDefaultSettings);
            SerializeData(universe);
            return universe;
        }

        /// <summary>
        /// This method should receive the Universe to add Points of Interest to and a set of POIDefaultSettings
        ///
        /// Default values are handled in POICreation.AddPOI
        /// </summary>
        /// <param name="universe"></param>
        /// <param name="poiDefaultSettings"></param>
        /// <returns>
        /// Return the newly edited Universe
        /// </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public Universe CreatePOI(Universe universe, POIDefaultSettings poiDefaultSettings)
        {
            // If there are no Planets or Locations for the Problems to be tied to then throw an exception
            if (universe.Stars == null || universe.Stars.Count == 0)
                throw new FileNotFoundException("No locations have been loaded.");

            // Set the Universe to the Universe return from ProblemCreation.AddProblems and serialize/return it
            universe = new POICreation().AddPOI(universe, poiDefaultSettings);
            SerializeData(universe);
            return universe;
        }

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

        /// <summary>
        /// This method receives the name of a Universe and deserializes it into a Universe object
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        /// The universe that matches the name specified
        /// </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public Universe LoadUniverse(string name)
        {
            // Set the path to the name
            var path = new StringBuilder();
            path.Append(_universePath + "/" + name + ".json");

            // If none exists throw an exception
            if (!File.Exists(path.ToString()))
                throw new FileNotFoundException(path + " not found.");

            // Parse the file into a JObject
            var univ =
                JObject.Parse(
                    File.ReadAllText(path.ToString()));

            // Deserialize the JObject into a Universe and return it
            return JsonConvert.DeserializeObject<Universe>(univ.ToString());
        }

        /// <summary>
        /// Method should receive a Universe that it will serialize to a file
        /// </summary>
        /// <param name="universe"></param>
        private void SerializeData(Universe universe)
        {
            // Set the path to the file and write it, overwriting the previous file if it exists.
            var path = _universePath + universe.Name + ".json";
            using var file =
                File.CreateText(path);
            var serializer = new JsonSerializer();
            serializer.Serialize(file, universe);
        }
    }
}