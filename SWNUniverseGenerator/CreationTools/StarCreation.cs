using System;
using System.Collections.Generic;
using System.Linq;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.DefaultSettings;
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
        /// <returns>
        /// A newly modified Universe
        /// </returns>
        public void AddStars(StarDefaultSettings starDefaultSettings)
        {
            using (var context = new UniverseContext())
            {
                using (var starRepo = new Repository<Star>(context))
                {
                    List<Star> stars = new();

                    // Set the number of stars to create. The default is 1d10+20 
                    var starCount = starDefaultSettings.StarCount;

                    var sCount = 0;
                    while (sCount < starCount)
                    {
                        var star = new Star()
                        {
                            UniverseId = starDefaultSettings.UniverseId
                        };

                        // Set Zone of the Star
                        using (var zoneRepo = new Repository<Zone>(context))
                        {
                            while (true)
                            {
                                var zoneId =
                                    ((Zone)zoneRepo.Random(z => z.UniverseId == starDefaultSettings.UniverseId)).Id;

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
                            using (var nameRepo = new Repository<Naming>(context))
                            {
                                star.Name = string.IsNullOrEmpty(starDefaultSettings.Name)
                                    ? ((Naming)nameRepo.Random(n => n.NameType == "Star")).Name
                                    : starDefaultSettings.Name;
                            }

                            // If that Name exists roll a new one 
                            if (!starRepo.Any(
                                    a => a.Name == star.Name && a.UniverseId == starDefaultSettings.UniverseId))
                                break;
                        }

                        // Set the Class of the Star
                        int starRand = Rand.Next(0, 100);
                        star.StarClass = starDefaultSettings.StarClass == Star.StarClassEnum.Undefined
                            ? starRand switch
                            {
                                >= 0 and < 1 => Star.StarClassEnum.O,
                                >= 1 and < 2 => Star.StarClassEnum.B,
                                >= 2 and < 3 => Star.StarClassEnum.A,
                                >= 3 and < 6 => Star.StarClassEnum.F,
                                >= 6 and < 13 => Star.StarClassEnum.G,
                                >= 13 and < 25 => Star.StarClassEnum.K,
                                _ => Star.StarClassEnum.M
                            }
                            : starDefaultSettings.StarClass;

                        // Set the Color of the Star
                        star.StarColor = starDefaultSettings.StarColor == Star.StarColorEnum.Undefined
                            ? starRand switch
                            {
                                >= 0 and < 1 => Star.StarColorEnum.Blue,
                                >= 1 and < 2 => Star.StarColorEnum.White,
                                >= 2 and < 3 => Star.StarColorEnum.Yellow,
                                >= 3 and < 6 => Star.StarColorEnum.LightOrange,
                                >= 6 and < 13 => Star.StarColorEnum.BlueWhite,
                                >= 13 and < 25 => Star.StarColorEnum.OrangeRed,
                                _ => Star.StarColorEnum.YellowWhite
                            }
                            : starDefaultSettings.StarColor;

                        // Add the Star to the Universe
                        stars.Add(star);

                        if (starDefaultSettings.CreatePlanets)
                        {
                            new PlanetCreation().AddPlanets(new PlanetDefaultSettings
                            {
                                UniverseId = starDefaultSettings.UniverseId,
                                StarList = new List<Star> { star }
                            });
                        }

                        sCount++;
                    }

                    starRepo.AddRange(stars);
                }
            }
        }
    }
}