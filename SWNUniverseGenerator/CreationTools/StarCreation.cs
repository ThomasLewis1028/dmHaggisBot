using System;
using System.Collections.Generic;
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
        private static readonly Random Rand = new Random();

        /// <summary>
        /// This function will receive a Universe and a StarDefaultSettings object and create Stars from the data
        /// provided
        /// </summary>
        /// <param name="universe"></param>
        /// <param name="starDefaultSettings"></param>
        /// <param name="starData"></param>
        /// <param name="nameGeneration"></param>
        /// <returns>
        /// A newly modified Universe
        /// </returns>
        public Universe AddStars(Universe universe, StarDefaultSettings starDefaultSettings, StarData starData,
            NameGeneration nameGeneration)
        {
            // Check if the Stars have been initialized prior
            universe.Stars ??= new List<Star>();

            // Set the number of stars to create. The default is 1d10+20 
            var starLen = starData.Stars.Count;
            var starCount = starDefaultSettings.StarCount;

            var sCount = 0;
            while (sCount < starCount)
            {
                var star = new Star();

                // Generate the unique ID for the Star
                IdGen.GenerateId(star);

                // Set Grid Location of the Star
                var zone = universe.Zones[Rand.Next(0, universe.Zones.Count)];
                if (zone.StarId == null)
                    zone.StarId = star.Id;
                else
                    continue;

                // If that ID exists roll a new one
                if (universe.Stars.Exists(a => a.Id == star.Id))
                    continue;

                // Pick a random Name for the Star
                star.Name = Rand.Next(0, 4) == 2
                    ? nameGeneration.GenerateName()
                    : starData.Stars[Rand.Next(0, starLen - 1)];

                // If that Name exists roll a new one 
                if (universe.Stars.Exists(a => a.Name == star.Name))
                    continue;

                // Set the color of the Star
                int starRand = Rand.Next(0, 100);
                int starClass = starDefaultSettings.StarClass == Star.StarClassEnum.Undefined
                    ? starRand switch
                    {
                        >= 0 and < 1 => (int) Star.StarClassEnum.O,
                        >= 1 and < 2 => (int) Star.StarClassEnum.B,
                        >= 2 and < 3 => (int) Star.StarClassEnum.A,
                        >= 3 and < 6 => (int) Star.StarClassEnum.F,
                        >= 6 and < 13 => (int) Star.StarClassEnum.G,
                        >= 13 and < 25 => (int) Star.StarClassEnum.K,
                        _ => (int) Star.StarClassEnum.M
                    }
                    : (int) starDefaultSettings.StarClass;
                
                star.StarClass = (Star.StarClassEnum) starClass;
                star.StarColor = (Star.StarColorEnum) starClass;

                // Add the Star to the Universe
                universe.Stars.Add(star);

                sCount++;
            }

            return universe;
        }
    }
}