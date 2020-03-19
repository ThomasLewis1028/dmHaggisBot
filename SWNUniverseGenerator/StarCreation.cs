using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SWNUniverseGenerator
{
    public class StarCreation
    {
        private static Random rand = new Random();

        public Universe AddStars(Universe universe, StarDefaultSettings starDefaultSettings)
        {
            if (universe.Stars == null)
                universe.Stars = new List<Star>();
            
            var worldInfo = LoadWorldInfo();
            var starData = LoadStarData();
            
            //Set sheet bounds inluding sheet# and row count.
            var starLen = starData.Stars.Count;
            var planLen = starData.Planets.Count;
            var starCount = string.IsNullOrEmpty(starDefaultSettings.StarCount) 
                ? Math.Floor(universe.Grid.X * universe.Grid.Y * .2)
                : Int32.Parse(starDefaultSettings.StarCount);

            int sCount = 0;
            while (sCount < starCount)
            {
                var starName = starData.Stars[rand.Next(0, starLen - 1)];
                Star star = new Star(
                    starName,
                    rand.Next(0, universe.Grid.X + 1), rand.Next(0, universe.Grid.Y + 1));

                int pCount = 0;
                int pMax = rand.Next(Int32.Parse(starDefaultSettings.PlanetRange[0]), Int32.Parse(starDefaultSettings.PlanetRange[1]) + 1);
                while (pCount < pMax)
                {
                    Planet planet =
                        new Planet(starData.Planets[rand.Next(0, planLen)]);
                    planet.WorldTag = worldInfo.WorldTags[rand.Next(0, 100)];
                    planet.Atmosphere = worldInfo.Atmospheres[rand.Next(0, 6) + rand.Next(0, 6)];
                    planet.Temperature = worldInfo.Temperatures[rand.Next(0, 6) + rand.Next(0, 6)];
                    planet.Biosphere = worldInfo.Biospheres[rand.Next(0, 6) + rand.Next(0, 6)];
                    planet.Population = worldInfo.Populations[rand.Next(0, 6) + rand.Next(0, 6)];
                    planet.TechLevel = worldInfo.TechLevels[rand.Next(0, 6) + rand.Next(0, 6)];
                    planet.Origin = worldInfo.OWOrigins[rand.Next(0, 8)];
                    planet.Relationship = worldInfo.OWRelationships[rand.Next(0, 8)];
                    planet.Contact = worldInfo.OWContacts[rand.Next(0, 8)];


                    star.Planets.Add(planet);
                    pCount++;
                }
                
                universe.Stars.Add(star);

                sCount++;
            }

            
            
            return universe;
        }

        private static WorldInfo LoadWorldInfo()
        {
            JObject tags =
                JObject.Parse(
                    File.ReadAllText(@"Data\worldTags.json"));

            return JsonConvert.DeserializeObject<WorldInfo>(tags.ToString());
        }

        private StarData LoadStarData()
        {
            JObject charData =
                JObject.Parse(
                    File.ReadAllText(@"Data\StarData.json"));

            return JsonConvert.DeserializeObject<StarData>(charData.ToString());
        }
    }
}