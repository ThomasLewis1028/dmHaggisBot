using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWNUniverseGenerator.CreationTools;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.DeserializedObjects;
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
        private static readonly string _localPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private readonly string _universePath = _localPath + "/UniverseFiles/";
        private readonly string _dataPath = _localPath + "/Data/";
        public const string universeExt = ".universe";

        public ShipData ShipData;
        private WorldInfo _worldInfo;
        private StarData _starData;
        private CharData _charData;
        public PoiData PoiData;
        public ProblemData ProblemData;
        public SocietyData SocietyData;
        public AlienData AlienData;

        public NameGeneration MaleFirstNameGeneration;
        public NameGeneration FemaleFirstNameGeneration;
        public NameGeneration LastNameGeneration;
        public List<NameGeneration> CharacterNameGenerations = new();

        public NameGeneration PlanetNameGeneration;
        public NameGeneration StarNameGeneration;

        /// <summary>
        /// Default constructor that requires a path to be passed in
        /// </summary>
        /// <param name="path"></param>
        public Creation()
        {
            ShipData = LoadData<ShipData>(@"shipData.json");
            _worldInfo = LoadData<WorldInfo>(@"worldTags.json");
            _starData = LoadData<StarData>(@"starData.json");
            _charData = LoadData<CharData>(@"characterData.json");
            PoiData = LoadData<PoiData>(@"pointsOfInterest.json");
            ProblemData = LoadData<ProblemData>(@"problemData.json");
            // SocietyData = LoadData<SocietyData>(@"societyData.json");
            SocietyData = null;
            AlienData = LoadData<AlienData>(@"alienData.json");

            MaleFirstNameGeneration = new NameGeneration();
            MaleFirstNameGeneration.GenerateChain(_charData.MaleName);

            FemaleFirstNameGeneration = new NameGeneration();
            FemaleFirstNameGeneration.GenerateChain(_charData.FemaleName);

            StarNameGeneration = new NameGeneration();
            StarNameGeneration.GenerateChain(_starData.Stars);

            PlanetNameGeneration = new NameGeneration();
            PlanetNameGeneration.GenerateChain(_starData.Planets);

            // LastNameGeneration = new NameGeneration();
            // LastNameGeneration.GenerateChain(_charData.LastName);

            CharacterNameGenerations.Add(MaleFirstNameGeneration);
            CharacterNameGenerations.Add(FemaleFirstNameGeneration);
            // CharacterNameGenerations.Add(LastNameGeneration);
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
            path.Append(_universePath + "\\" + name + universeExt);

            // Look for the file based on the path above
            if (File.Exists(path.ToString()))
            {
                // If the Overwrite Tag says "Y" then overwrite the file
                if (universeDefaultSettings.Overwrite)
                    File.Delete(path.ToString());
                // Otherwise throw an exception to be caught
                else
                    throw new IOException($"{universeDefaultSettings.Name} already exists.");
            }

            // Close the file so that it can be written to by the rest of the program
            File.Create(path.ToString()).Close();

            // Set the grid to the specified values or the default [8, 10]
            var gridX = universeDefaultSettings.GridX ?? 8;
            var gridY = universeDefaultSettings.GridY ?? 10;

            // Create the Universe.
            var universe = new Universe(name, gridX, gridY)
            {
                Zones = new List<Zone>(),
                Stars = new List<Star>(),
                Planets = new List<Planet>(),
                PointsOfInterest = new List<PointOfInterest>(),
                Characters = new List<Character>(),
                Problems = new List<Problem>(),
                Ships = new List<Ship>(),
                Aliens = new List<Alien>()
            };

            // Add the Zones to the Universe
            for (var i = 0; i < gridY; i++)
            {
                for (var j = 0; j < gridX; j++)
                {
                    Zone zone = new Zone
                    {
                        X = j, Y = i, Planets = new List<Planet>(), PointsOfInterest = new List<PointOfInterest>()
                    };
                    IdGen.GenerateId(zone);
                    universe.Zones.Add(zone);
                }
            }

            // Serialize and return the Universe
            SerializeData(universe);
            return universe;
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
            if (universe.GridX == null || universe.GridY == null)
                throw new FileNotFoundException("No grid has been set for the universe");

            // Set the Universe to the Universe returned from StarCreation.AddStars and serialize/return it
            universe = new StarCreation().AddStars(universe, starDefaultSettings, _starData, StarNameGeneration);
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
            universe = new PlanetCreation().AddPlanets(universe, planetDefaultSettings, _worldInfo, _starData,
                SocietyData, PlanetNameGeneration);
            SerializeData(universe);
            return universe;
        }

        /// <summary>
        /// This method should receive the Universe to add Ships to and a set of ShipDefaultSettings
        /// 
        /// Default values are handled in ShipCreation.AddShips
        /// </summary>
        /// <param name="universe"></param>
        /// <param name="shipDefaultSettings"></param>
        /// <returns>
        /// Return the newly edited Universe
        /// </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public Universe CreateShips(Universe universe, ShipDefaultSettings shipDefaultSettings)
        {
            // If there are no Planets for the Ships to be tied to then throw an exception
            if (universe.Planets == null || universe.Planets.Count == 0)
                throw new FileNotFoundException("No planets have been created for the universe");

            // Set the Universe to the Universe returned from CharCreation.AddCharacters and serialize/return it
            universe = new ShipCreation().AddShips(universe, shipDefaultSettings, ShipData, _charData,
                CharacterNameGenerations);
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
            universe = new CharCreation().AddCharacters(universe, characterDefaultSettings, _charData,
                CharacterNameGenerations);
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
            universe = new ProblemCreation().AddProblems(universe, problemDefaultSettings, ProblemData);
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
        public Universe CreatePoi(Universe universe, PoiDefaultSettings poiDefaultSettings)
        {
            // If there are no Planets or Locations for the Problems to be tied to then throw an exception
            if (universe.Stars == null || universe.Stars.Count == 0)
                throw new FileNotFoundException("No locations have been loaded.");

            // Set the Universe to the Universe return from ProblemCreation.AddProblems and serialize/return it
            universe = new PoiCreation().AddPoi(universe, poiDefaultSettings, PoiData);
            SerializeData(universe);
            return universe;
        }

        /// <summary>
        /// This method should receive the Universe to add Aliens to and a set of AlienDefaultSettings
        ///
        /// Default values are handled in AlienCreation.AddAliens
        /// </summary>
        /// <param name="universe"></param>
        /// <param name="alienDefaultSettings"></param>
        /// <returns>
        /// Return the newly edited Universe
        /// </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public Universe CreateAliens(Universe universe, AlienDefaultSettings alienDefaultSettings)
        {
            // If there are no Planets or Locations for the Problems to be tied to then throw an exception
            if (universe.Stars == null || universe.Stars.Count == 0)
                throw new FileNotFoundException("No locations have been loaded.");

            // Set the Universe to the Universe return from ProblemCreation.AddProblems and serialize/return it
            universe = new AlienCreation().AddAliens(universe, alienDefaultSettings, AlienData);
            SerializeData(universe);
            return universe;
        }

        /// <summary>
        /// This method receives a universe file and generates an image in the background
        /// </summary>
        /// <param name="universe"></param>
        /// <exception cref="FileNotFoundException"></exception>
        public void CreateStarMap(Universe universe)
        {
            // If there are no Planets or Locations for the Problems to be tied to then throw an exception
            if (universe.Planets == null || universe.Planets.Count == 0)
                throw new FileNotFoundException("No locations have been loaded.");

            new GridCreation().CreateGrid(universe);
        }

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
            path.Append(_universePath + "/" + name + universeExt);

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

        public void DeleteUniverse(string universeName)
        {
            // Set the path to the name
            var path = new StringBuilder();
            path.Append(_universePath + "/" + universeName + universeExt);
            
            // If none exists throw an exception
            if (!File.Exists(path.ToString()))
                throw new FileNotFoundException(path + " not found.");
            
            File.Delete(path.ToString());
        }

        /// <summary>
        /// Method should receive a Universe that it will serialize to a file
        /// </summary>
        /// <param name="universe"></param>
        private void SerializeData(Universe universe)
        {
            // Set the path to the file and write it, overwriting the previous file if it exists.
            var path = _universePath + universe.Name + universeExt;
            using var file =
                File.CreateText(path);
            var serializer = new JsonSerializer();
            serializer.Serialize(file, universe);
        }

        /// <summary>
        /// Receive the path to a data type and return the deserialized version of that data
        /// </summary>
        /// <param name="path"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T LoadData<T>(String path)
        {
            path = _dataPath + path;

            var data =
                JObject.Parse(
                    File.ReadAllText(path));

            return JsonConvert.DeserializeObject<T>(data.ToString());
        }

        /// <summary>
        /// Retrieve all universe files on disk
        /// </summary>
        /// <returns></returns>
        public List<UniverseInfo> GetUniverseList()
        {
            List<UniverseInfo> universeInfos = new();
            string[] fileListFullPath = Directory.GetFiles(_universePath, "*" + universeExt);

            foreach (var fullPath in fileListFullPath)
            {
                var filename = Path.GetFileName(fullPath).Replace(universeExt, "");
                
                Universe universe = LoadUniverse(filename);
                UniverseInfo universeInfo = new UniverseInfo()
                {
                    Name = universe.Name,
                    GridX = universe.GridX,
                    GridY = universe.GridY,
                    StarCount = universe.Stars.Count,
                    PlanetCount = universe.Planets.Count,
                    ShipCount = universe.Ships.Count,
                    CharCount = universe.Characters.Count
                };
                universeInfos.Add(universeInfo);
            }

            return universeInfos;
        }

        /// <summary>
        /// Class to hold information for brief display.
        /// </summary>
        public class UniverseInfo
        {
            public String Name { get; set; }

            public int? GridX;
            public int? GridY;

            public int StarCount;
            public int PlanetCount;
            public int ShipCount;
            public int CharCount;
        }
    }
}