using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.DeserializedObjects;
using SWNUniverseGenerator.Models;

// using ProblemData = SWNUniverseGenerator.DeserializedObjects.ProblemData;

namespace SWNUniverseGenerator.CreationTools
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

        // public ProblemData ProblemData;
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
        public Creation()
        {
            // TODO: Fix name generation scripts
            // MaleFirstNameGeneration = new NameGeneration();
            // MaleFirstNameGeneration.GenerateChain(_charData.MaleName);
            //
            // FemaleFirstNameGeneration = new NameGeneration();
            // FemaleFirstNameGeneration.GenerateChain(_charData.FemaleName);
            //
            // StarNameGeneration = new NameGeneration();
            // StarNameGeneration.GenerateChain(_starData.Stars);
            //
            // PlanetNameGeneration = new NameGeneration();
            // PlanetNameGeneration.GenerateChain(_starData.Planets);

            // LastNameGeneration = new NameGeneration();
            // LastNameGeneration.GenerateChain(_charData.LastName);

            // CharacterNameGenerations.Add(MaleFirstNameGeneration);
            // CharacterNameGenerations.Add(FemaleFirstNameGeneration);
            // CharacterNameGenerations.Add(LastNameGeneration);
        }

        /// <summary>
        /// This requires a set of UniverseDefaultSettings to create a Universe
        /// 
        /// If no names or grids are set use the defaults of "Universe" and [8, 10]
        /// </summary>
        /// <param name="universeDefaultSettings"></param>
        /// <param name="context"></param>
        /// <returns>The newly created Universe</returns>
        /// <exception cref="IOException"></exception>
        public string CreateUniverse(UniverseDefaultSettings universeDefaultSettings, UniverseContext context)
        {
            string universeId = GenerateUniverse(universeDefaultSettings, context);

            GenerateZones(universeDefaultSettings, context, universeId);

            return universeId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="universeDefaultSettings"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private static string GenerateUniverse(UniverseDefaultSettings universeDefaultSettings, UniverseContext context)
        {
            // Create the Universe with the values specified, or defaults
            var universe = new Universe
            {
                Name = universeDefaultSettings.Name,
                GridX = universeDefaultSettings.GridX,
                GridY = universeDefaultSettings.GridY
            };

            // Add the Universe to the database
            using (var univRepo = new Repository<Universe>(context))
                univRepo.Add(universe);

            return universe.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="universeDefaultSettings"></param>
        /// <param name="context"></param>
        /// <param name="universeId"></param>
        private static bool GenerateZones(UniverseDefaultSettings universeDefaultSettings, UniverseContext context,
            string universeId)
        {
            List<Zone> zones = new();
            // Add the Zones to the Universe
            for (var i = 0; i < universeDefaultSettings.GridX; i++)
            {
                for (var j = 0; j < universeDefaultSettings.GridY; j++)
                {
                    Zone zone = new Zone
                    {
                        X = i,
                        Y = j,
                        UniverseId = universeId
                    };

                    zones.Add(zone);
                }
            }

            using var zoneRepo = new Repository<Zone>(context);
            bool result = zoneRepo.AddRange(zones);
            
            return result;
        }

        /// <summary>
        /// This method should receive the Universe to add Stars to and a set of StarDefaultSettings
        /// 
        /// Default values are handled in StarCreation.AddStars
        /// </summary>
        /// <param name="universeId"></param>
        /// <param name="starDefaultSettings"></param>
        /// <param name="context"></param>
        /// <returns>
        /// True at the end
        /// </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public bool CreateStars(String universeId, StarDefaultSettings starDefaultSettings)
        {
            // Set the Universe to the Universe returned from StarCreation.AddStars and serialize/return it
            new StarCreation().AddStars(universeId, starDefaultSettings);

            return true;
        }

        /// <summary>
        /// This method should receive the Universe to add Planets to and a set of PlanetDefaultSettings
        ///
        /// Default values are handled in PlanetCreation.AddPlanets
        /// </summary>
        /// <param name="universeId"></param>
        /// <param name="planetDefaultSettings"></param>
        /// <returns>
        /// Return the newly edited Universe
        /// </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public bool CreatePlanets(String universeId, PlanetDefaultSettings planetDefaultSettings)
        {
            // Set the Universe to the Universe returned from PlanetCreation.AddPlanets and serialize/return it
            new PlanetCreation().AddPlanets(universeId, planetDefaultSettings);

            return true;
        }

        /// <summary>
        /// This method should receive the Universe to add Ships to and a set of ShipDefaultSettings
        /// 
        /// Default values are handled in ShipCreation.AddShips
        /// </summary>
        /// <param name="universeId"></param>
        /// <param name="shipDefaultSettings"></param>
        /// <returns>
        /// Return the newly edited Universe
        /// </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public bool CreateShips(String universeId, ShipDefaultSettings shipDefaultSettings)
        {
            // Set the Universe to the Universe returned from CharCreation.AddCharacters and serialize/return it
            new ShipCreation().AddShips(universeId, shipDefaultSettings);

            return true;
        }

        /// <summary>
        /// This method should receive the Universe to add Characters to and a set of CharacterDefaultSettings
        ///
        /// Default values are handled in CharCreation.AddCharacters
        /// </summary>
        /// <param name="universeId"></param>
        /// <param name="characterDefaultSettings"></param>
        /// <returns>
        /// Return the newly edited Universe
        /// </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public bool CreateCharacter(String universeId, CharacterDefaultSettings characterDefaultSettings)
        {
            // Set the Universe to the Universe returned from CharCreation.AddCharacters and serialize/return it
            new CharCreation().AddCharacters(universeId, characterDefaultSettings);

            return true;
        }

        /// <summary>
        /// This method should receive the Universe to add Problems to and a set of ProblemDefaultSettings
        ///
        /// Default values are handled in ProblemCreation.AddProblems
        /// </summary>
        /// <param name="universeId"></param>
        /// <param name="problemDefaultSettings"></param>
        /// <returns>
        /// Return the newly edited Universe
        /// </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public bool CreateProblems(String universeId, ProblemDefaultSettings problemDefaultSettings)
        {
            // Set the Universe to the Universe return from ProblemCreation.AddProblems and serialize/return it
            // new ProblemCreation().AddProblems(universeId, problemDefaultSettings);

            return true;
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
            // // If there are no Planets or Locations for the Problems to be tied to then throw an exception
            // if (universe.Stars == null || universe.Stars.Count == 0)
            //     throw new FileNotFoundException("No locations have been loaded.");
            //
            // // Set the Universe to the Universe return from ProblemCreation.AddProblems and serialize/return it
            // universe = new PoiCreation().AddPoi(universe, poiDefaultSettings, PoiData);
            // SerializeData(universe);
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
            // // If there are no Planets or Locations for the Problems to be tied to then throw an exception
            // if (universe.Stars == null || universe.Stars.Count == 0)
            //     throw new FileNotFoundException("No locations have been loaded.");
            //
            // // Set the Universe to the Universe return from ProblemCreation.AddProblems and serialize/return it
            // universe = new AlienCreation().AddAliens(universe, alienDefaultSettings, AlienData);
            // SerializeData(universe);
            return universe;
        }

        /// <summary>
        /// This method receives a universe file and generates an image in the background
        /// </summary>
        /// <param name="universeId"></param>
        /// <exception cref="FileNotFoundException"></exception>
        public String CreateStarMap(String universeId)
        {
            // If there are no Planets or Locations for the Problems to be tied to then throw an exception
            using var context = new UniverseContext();
            using var planRepo = new Repository<Planet>(context);

            return new GridCreation().CreateGrid(universeId, context);
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

        /// <summary>
        /// This method receives the ID for a universe and deletes the entire universe as needed
        /// </summary>
        /// <param name="universeId"></param>
        public void DeleteUniverse(String universeId)
        {
            File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                        + "/"
                        + universeId
                        + ".png");

            using (var context = new UniverseContext())
            {
                using (var crewRepo = new Repository<CrewMember>(context))
                    crewRepo.DeleteRange(context.CrewMember.Where(c => c.UniverseId == universeId).ToList());

                using (var charRepo = new Repository<Character>(context))
                    charRepo.DeleteRange(context.Characters.Where(c => c.UniverseId == universeId).ToList());

                using (var armaRepo = new Repository<ShipArmament>(context))
                    armaRepo.DeleteRange(context.ShipArmament.Where(c => c.UniverseId == universeId).ToList());

                using (var defRepo = new Repository<ShipDefense>(context))
                    defRepo.DeleteRange(context.ShipDefense.Where(c => c.UniverseId == universeId).ToList());

                using (var fitRepo = new Repository<ShipFitting>(context))
                    fitRepo.DeleteRange(context.ShipFitting.Where(c => c.UniverseId == universeId).ToList());

                using (var shipRepo = new Repository<Ship>(context))
                    shipRepo.DeleteRange(context.Ships.Where(c => c.UniverseId == universeId).ToList());

                using (var planRepo = new Repository<Planet>(context))
                    planRepo.DeleteRange(context.Planets.Where(c => c.UniverseId == universeId).ToList());

                using (var starRepo = new Repository<Star>(context))
                    starRepo.DeleteRange(context.Stars.Where(c => c.UniverseId == universeId).ToList());

                using (var zoneRepo = new Repository<Zone>(context))
                    zoneRepo.DeleteRange(context.Zones.Where(c => c.UniverseId == universeId).ToList());

                using (var uniRepo = new Repository<Universe>(context))
                    uniRepo.Delete(universeId);
            }
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
                    // Name = universe.Name,
                    // GridX = universe.GridX,
                    // GridY = universe.GridY,
                    // StarCount = universe.Stars.Count,
                    // PlanetCount = universe.Planets.Count,
                    // ShipCount = universe.Ships.Count,
                    // CharCount = universe.Characters.Count
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