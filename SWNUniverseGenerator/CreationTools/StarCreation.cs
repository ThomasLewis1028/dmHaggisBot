using System;
using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;
using SWNUniverseGenerator.Database;
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
        /// <param name="universeId"></param>
        /// <param name="starDefaultSettings"></param>
        /// <param name="starData"></param>
        /// <param name="nameGeneration"></param>
        /// <returns>
        /// A newly modified Universe
        /// </returns>
        public void AddStars(String universeId, StarDefaultSettings starDefaultSettings, StarData starData,
            NameGeneration nameGeneration)
        {
            using (var context = new UniverseContext())
            {
                using (var starRepo = new Repository<Star>(context))
                {

                    // Set the number of stars to create. The default is 1d10+20 
                    var starLen = starData.Stars.Count;
                    var starCount = starDefaultSettings.StarCount;

                    var sCount = 0;
                    while (sCount < starCount)
                    {
                        var star = new Star();

                        // Set Zone of the Star
                        using (var zoneRepo = new Repository<Zone>(context))
                        {
                            List<IEntity> zones = zoneRepo.Search(z => z.UniverseId == universeId).ToList();
                            
                            while(true)
                            {
                                var zoneId = zones[Rand.Next(0, zones.Count)].Id;

                                if (starRepo.Search(s => s.ZoneId == zoneId).Any()) 
                                    continue;
                                
                                star.ZoneId = zoneId;
                                break;
                            }
                        }
                        
                        // Name the Star
                        while (true)
                        {
                            // Pick a random Name for the Star
                            star.Name = Rand.Next(0, 4) == 2
                                ? nameGeneration.GenerateName()
                                : starData.Stars[Rand.Next(0, starLen - 1)];

                            // If that Name exists roll a new one 
                            if (!starRepo.Any(a => a.Name == star.Name))
                                break;
                        }
                        
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
                        starRepo.Add(star);

                        sCount++;
                    }
                }
            }
        }
    }
}