using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.DeserializedObjects;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.CreationTools
{
    /// <summary>
    /// This class holds all the necessary functions for creating Planets and adding them to the Universe
    /// </summary>
    public class PlanetCreation
    {
        private static readonly Random rand = new Random();

        /// <summary>
        /// This function receives a Universe and a PlanetDefaultSettings to create planets based on
        /// specified information
        /// </summary>
        /// <param name="universe"></param>
        /// <param name="planetDefaultSettings"></param>
        /// <returns>
        /// The newly updated Universe
        /// </returns>
        public Universe AddPlanets(Universe universe, PlanetDefaultSettings planetDefaultSettings)
        {
            // Load the information about World Tags and the Planet Names
            var worldInfo = LoadWorldInfo();
            var starData = LoadStarData();
            
            // Get the number of Planet Names from starData
            var planLen = starData.Planets.Count;
            
            // Iterate through each star and 
            foreach(Star star in universe.Stars){
                // Set the random number of Planets that will be created for a given Star 
                var pMax = planetDefaultSettings.PlanetRange == null || planetDefaultSettings.PlanetRange.Length == 0 ||
                           planetDefaultSettings.PlanetRange[0] == 0 ||
                           planetDefaultSettings.PlanetRange[1] == 0
                    ? rand.Next(0, 4) // Default Planet count is up-to 4
                    : rand.Next(planetDefaultSettings.PlanetRange[0],
                        planetDefaultSettings.PlanetRange[1] + 1);

                var pCount = 0;

                while (pCount < pMax)
                {
                    var planet = new Planet();
                    
                    // Generate the ID for the Planet
                    IDGen.GenerateID(planet);
                    
                    // If that ID exists somewhere then re-roll it
                    if (universe.Planets.Exists(a => a.ID == planet.ID))
                        continue;
                    
                    // Pick a random name out of the list of Planets
                    planet.Name = starData.Planets[rand.Next(0, planLen)];
                    
                    // No planets can share a name
                    if (universe.Planets.Exists(a => a.Name == planet.Name))
                        continue;
                    
                    // Set the Planet information from either a randomized value or specified information
                    planet.StarID = star.ID;
                    planet.FirstWorldTag = worldInfo.WorldTags[rand.Next(0, 100)];
                    planet.SecondWorldTag = worldInfo.WorldTags[rand.Next(0, 100)];
                    planet.Atmosphere = worldInfo.Atmospheres[rand.Next(0, 6) + rand.Next(0, 6)];
                    planet.Temperature = worldInfo.Temperatures[rand.Next(0, 6) + rand.Next(0, 6)];
                    planet.Biosphere = worldInfo.Biospheres[rand.Next(0, 6) + rand.Next(0, 6)];
                    planet.Population = worldInfo.Populations[rand.Next(0, 6) + rand.Next(0, 6)];
                    planet.TechLevel = worldInfo.TechLevels[rand.Next(0, 6) + rand.Next(0, 6)];
                    planet.Origin = worldInfo.OWOrigins[rand.Next(0, 8)];
                    planet.Relationship = worldInfo.OWRelationships[rand.Next(0, 8)];
                    planet.Contact = worldInfo.OWContacts[rand.Next(0, 8)];

                    // Add the Planet to the current Universe
                    universe.Planets.Add(planet);
                    pCount++;
                }
            }

            return universe;
        }
        
        /// <summary>
        /// This grabs all of the information out of the worldTags.json and places it into a WorldInfo object
        /// after deserializing it.
        /// </summary>
        /// <returns>
        /// A deserialized WorldInfo object
        /// </returns>
        private WorldInfo LoadWorldInfo()
        {
            var tags =
                JObject.Parse(
                    File.ReadAllText(@"Data/worldTags.json"));

            return JsonConvert.DeserializeObject<WorldInfo>(tags.ToString());
        }
        
        /// <summary>
        /// This grabs all of the information out of the StarData.json and places it into a StarData object
        /// after deserializing it.
        /// </summary>
        /// <returns>
        /// A deserialized StarData object
        /// </returns>
        private StarData LoadStarData()
        {
            var charData =
                JObject.Parse(
                    File.ReadAllText(@"Data/starData.json"));

            return JsonConvert.DeserializeObject<StarData>(charData.ToString());
        }

    }
}