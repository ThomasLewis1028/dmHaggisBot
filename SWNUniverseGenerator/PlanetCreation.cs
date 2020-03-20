using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SWNUniverseGenerator
{
    public class PlanetCreation
    {
        private static readonly Random rand = new Random();

        public Universe AddPlanets(Universe universe, PlanetDefaultSettings planetDefaultSettings)
        {
            var worldInfo = LoadWorldInfo();
            var starData = LoadStarData();
            
            var planLen = starData.Planets.Count;
            
            foreach(Star star in universe.Stars){
                var pMax = planetDefaultSettings.PlanetRange == null || planetDefaultSettings.PlanetRange.Length == 0 ||
                           planetDefaultSettings.PlanetRange[0] == 0 ||
                           planetDefaultSettings.PlanetRange[1] == 0
                    ? 4
                    : rand.Next(planetDefaultSettings.PlanetRange[0],
                        planetDefaultSettings.PlanetRange[1] + 1);

                var pCount = 0;

                while (pCount < pMax)
                {
                    var planet =
                        new Planet(starData.Planets[rand.Next(0, planLen)]);

                    if (universe.Planets.Exists(a => a.Name == planet.Name))
                        continue;


                    IDGen.GenerateID(planet);

                    planet.StarID = star.ID;
                    planet.WorldTag = worldInfo.WorldTags[rand.Next(0, 100)];
                    planet.Atmosphere = worldInfo.Atmospheres[rand.Next(0, 6) + rand.Next(0, 6)];
                    planet.Temperature = worldInfo.Temperatures[rand.Next(0, 6) + rand.Next(0, 6)];
                    planet.Biosphere = worldInfo.Biospheres[rand.Next(0, 6) + rand.Next(0, 6)];
                    planet.Population = worldInfo.Populations[rand.Next(0, 6) + rand.Next(0, 6)];
                    planet.TechLevel = worldInfo.TechLevels[rand.Next(0, 6) + rand.Next(0, 6)];
                    planet.Origin = worldInfo.OWOrigins[rand.Next(0, 8)];
                    planet.Relationship = worldInfo.OWRelationships[rand.Next(0, 8)];
                    planet.Contact = worldInfo.OWContacts[rand.Next(0, 8)];

                    universe.Planets.Add(planet);
                    pCount++;
                }
            }

            return universe;
        }
        
        private WorldInfo LoadWorldInfo()
        {
            var tags =
                JObject.Parse(
                    File.ReadAllText(@"Data\worldTags.json"));

            return JsonConvert.DeserializeObject<WorldInfo>(tags.ToString());
        }
        
        private StarData LoadStarData()
        {
            var charData =
                JObject.Parse(
                    File.ReadAllText(@"Data\StarData.json"));

            return JsonConvert.DeserializeObject<StarData>(charData.ToString());
        }

    }
}