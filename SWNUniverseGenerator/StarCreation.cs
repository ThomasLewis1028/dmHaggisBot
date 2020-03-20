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

            var starData = LoadStarData();

            var starLen = starData.Stars.Count;
            var starCount = starDefaultSettings.StarCount == 0
                ? Math.Floor(universe.Grid.X * universe.Grid.Y * .2)
                : starDefaultSettings.StarCount;

            var sCount = 0;
            while (sCount < starCount)
            {
                var starName = starData.Stars[rand.Next(0, starLen - 1)];
                var star = new Star(
                    starName,
                    rand.Next(0, universe.Grid.X + 1), rand.Next(0, universe.Grid.Y + 1));

                if (universe.Stars.Exists(a => a.Name == star.Name))
                    continue;
                
                IDGen.GenerateID(star);

                universe.Stars.Add(star);

                sCount++;
            }

            return universe;
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