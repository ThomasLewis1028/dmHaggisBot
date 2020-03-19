using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SWNUniverseGenerator
{
    internal class StarCreation
    {
        private static readonly Random rand = new Random();

        public Universe AddStars(Universe universe, StarDefaultSettings starDefaultSettings)
        {
            if (universe.Stars == null)
                universe.Stars = new List<Star>();
            
            if (universe.Planets == null)
                universe.Planets = new List<Planet>();

            var worldInfo = LoadWorldInfo();
            var starData = LoadStarData();

            var starLen = starData.Stars.Count;
            var planLen = starData.Planets.Count;
            var starCount = string.IsNullOrEmpty(starDefaultSettings.StarCount)
                ? Math.Floor(universe.Grid.X * universe.Grid.Y * .2)
                : int.Parse(starDefaultSettings.StarCount);

            var sCount = 0;
            while (sCount < starCount)
            {
                var starName = starData.Stars[rand.Next(0, starLen - 1)];
                var star = new Star(
                    starName,
                    rand.Next(0, universe.Grid.X + 1), rand.Next(0, universe.Grid.Y + 1));
                IDGen.GenerateID(star);
                
                if (universe.Stars.Exists(a => a.Name == star.Name))
                    continue;

                var pCount = 0;
                var pMax = rand.Next(int.Parse(starDefaultSettings.PlanetRange[0]),
                    int.Parse(starDefaultSettings.PlanetRange[1]) + 1);
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

                universe.Stars.Add(star);

                sCount++;
            }

            return universe;
        }

        private static WorldInfo LoadWorldInfo()
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