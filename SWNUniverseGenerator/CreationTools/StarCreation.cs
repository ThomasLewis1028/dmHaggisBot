using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.DeserializedObjects;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.CreationTools
{
    /// <summary>
    /// This class contains the necessary functions for creating a Star
    /// </summary>
    internal class StarCreation
    {
        private static readonly Random rand = new Random();

        /// <summary>
        /// This function will receive a Universe and a StarDefaultSettings object and create Stars from the data
        /// provided
        /// </summary>
        /// <param name="universe"></param>
        /// <param name="starDefaultSettings"></param>
        /// <returns>
        /// A newly modified Universe
        /// </returns>
        public Universe AddStars(Universe universe, StarDefaultSettings starDefaultSettings)
        {
            // Check if the Stars have been initialized prior
            if (universe.Stars == null)
                universe.Stars = new List<Star>();

            // Load the StarData
            var starData = LoadStarData();

            // Set the number of stars to create. The default is 1d10+20 
            var starLen = starData.Stars.Count;
            var starCount = starDefaultSettings.StarCount < 0
                ? rand.Next(0, 10) + 20
                : starDefaultSettings.StarCount;

            var sCount = 0;
            while (sCount < starCount)
            {
                var star = new Star();

                // Generate the unique ID for the Star
                IDGen.GenerateID(star);
                
                // If that ID exists roll a new one
                if (universe.Stars.Exists(a => a.ID == star.ID))
                    continue;
                
                // Pick a random Name for the Star
                star.Name = starData.Stars[rand.Next(0, starLen - 1)];
                
                // If that Name exists roll a new one 
                if (universe.Stars.Exists(a => a.Name == star.Name))
                    continue;
                
                // Set the type of Star
                star.StarType = starData.StarTypes[rand.Next(0, 8) + rand.Next(0, 8) + rand.Next(0, 8)];
                
                // Set Grid Location of the Star
                star.X = rand.Next(0, universe.Grid.X + 1);
                star.Y = rand.Next(0, universe.Grid.Y + 1);

                // Add the Star to the Universe
                universe.Stars.Add(star);

                sCount++;
            }

            return universe;
        }


        /// <summary>
        /// Load the Data needed about Stars into a StarData object
        /// </summary>
        /// <returns>
        /// A StarData object
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