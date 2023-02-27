﻿using System;
using System.Collections.Generic;
using System.Linq;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.DeserializedObjects;
using SWNUniverseGenerator.Models;
using Ruled = SWNUniverseGenerator.Models.Ruled;

namespace SWNUniverseGenerator.CreationTools
{
    /// <summary>
    /// This class holds all the necessary functions for creating Planets and adding them to the Universe
    /// </summary>
    internal class PlanetCreation
    {
        private static readonly Random Rand = new();

        /// <summary>
        /// This function receives a Universe and a PlanetDefaultSettings to create planets based on
        /// specified information
        /// </summary>
        /// <param name="universeId"></param>
        /// <param name="planetDefaultSettings"></param>
        /// <param name="worldInfo"></param>
        /// <param name="starData"></param>
        /// <param name="societyData"></param>
        /// <param name="nameGeneration"></param>
        /// <returns>
        /// The newly updated Universe
        /// </returns>
        public void AddPlanets(String universeId, PlanetDefaultSettings planetDefaultSettings, WorldInfo worldInfo,
            StarData starData, SocietyData societyData, NameGeneration nameGeneration)
        {
            using (var context = new UniverseContext())
            {
                using (var planetRepo = new Repository<Planet>(context))
                {
                    // Get the number of Planet Names from starData
                    var planLen = starData.Planets.Count;

                    List<IEntity> stars;

                    using (var starRepo = new Repository<Star>(context))
                    {
                        stars = starRepo.Search(s => s.UniverseId == universeId).ToList();
                    }

                    // Iterate through each star and add planets
                    foreach (var entity in stars)
                    {
                        var star = (Star) entity;

                        // Set the random number of Planets that will be created for a given Star 
                        var pMax = planetDefaultSettings.PlanetRange == null ||
                                   planetDefaultSettings.PlanetRange.Length == 0 ||
                                   planetDefaultSettings.PlanetRange[0] == 0 ||
                                   planetDefaultSettings.PlanetRange[1] == 0
                            ? Rand.Next(1, 3) // Default Planet count is up-to 3
                            : Rand.Next(planetDefaultSettings.PlanetRange[0],
                                planetDefaultSettings.PlanetRange[1] + 1);

                        var pCount = 0;

                        while (pCount < pMax)
                        {
                            var planet = new Planet
                            {
                                // Society = new Society(), Ruler = new Ruler(), Ruled = new Ruled(), Flavor = new Flavor()
                            };

                            // Name the Planet
                            while (true)
                            {
                                // Pick a random name out of the list of Planets
                                planet.Name = Rand.Next(0, 4) == 2
                                    ? nameGeneration.GenerateName()
                                    : starData.Planets[Rand.Next(0, planLen)];

                                // No planets can share a name
                                if (!planetRepo.Any(a => a.Name == planet.Name))
                                    break;
                            }

                            // Set the Planet information from either a randomized value or specified information
                            planet.ZoneId = star.ZoneId;
                            planet.FirstWorldTag = worldInfo.WorldTags[Rand.Next(0, 100)].Type;
                            planet.SecondWorldTag = worldInfo.WorldTags[Rand.Next(0, 100)].Type;
                            planet.Atmosphere = worldInfo.Atmospheres[Rand.Next(0, 6) + Rand.Next(0, 6)].Type;
                            planet.Temperature = worldInfo.Temperatures[Rand.Next(0, 6) + Rand.Next(0, 6)].Type;
                            planet.Biosphere = worldInfo.Biospheres[Rand.Next(0, 6) + Rand.Next(0, 6)].Type;
                            planet.Population = worldInfo.Populations[Rand.Next(0, 6) + Rand.Next(0, 6)].Type;
                            planet.TechLevel = worldInfo.TechLevels[Rand.Next(0, 6) + Rand.Next(0, 6)].Type;

                            // Set primary world
                            if (pCount == 0)
                                planet.IsPrimary = true;
                            else
                            {
                                // Non-primary world information
                                planet.Origin = worldInfo.OwOrigins[Rand.Next(0, 8)];
                                planet.Relationship = worldInfo.OwRelationships[Rand.Next(0, 8)];
                                planet.Contact = worldInfo.OwContacts[Rand.Next(0, 8)];
                                planet.IsPrimary = false;
                            }

                            // Add the Planet to the current Universe
                            planetRepo.Add(planet);
                            pCount++;
                        }
                    }
                }
            }
        }
    }
}

// TODO: Reimplement this stuff, maybe?
// if(societyData != null)
// {
//     // Set the Planet's society information
//     planet.Society.PriorCulture = societyData.Societies.PriorCultures[Rand.Next(0, 6)];
//     planet.Society.OtherSociety = societyData.Societies.OtherSocieties[Rand.Next(0, 8)];
//     planet.Society.MainRemnant = societyData.Societies.MainRemnants[Rand.Next(0, 10)];
//     planet.Society.SocietyAge = societyData.Societies.SocietyAges[Rand.Next(0, 4)];
//     planet.Society.ImportantResource = societyData.Societies.ImportantResources[Rand.Next(0, 12)];
//     planet.Society.FoundingReason = societyData.Societies.FoundingReasons[Rand.Next(0, 20)];
//
//     // Set the Planet's Ruler
//     planet.Ruler.GeneralSecurity = societyData.Rulers.GeneralSecurities[Rand.Next(0, 6)];
//     planet.Ruler.LegitimacySource = societyData.Rulers.LegitimacySources[Rand.Next(0, 8)];
//     planet.Ruler.MainRulerConflict = societyData.Rulers.MainRulerConflicts[Rand.Next(0, 10)];
//     planet.Ruler.RuleCompletion = societyData.Rulers.RuleCompletions[Rand.Next(0, 4)];
//     planet.Ruler.RuleForm = societyData.Rulers.RuleForms[Rand.Next(0, 12)];
//     planet.Ruler.MainPopConflict = societyData.Rulers.MainPopConflicts[Rand.Next(0, 20)];
//
//     // Set the Planet's Ruled
//     planet.Ruled.Contentment = societyData.Ruled.Contentments[Rand.Next(0, 6)];
//     planet.Ruled.LastMajorThreat = societyData.Ruled.LastMajorThreats[Rand.Next(0, 8)];
//     planet.Ruled.Power = societyData.Ruled.Powers[Rand.Next(0, 10)];
//     planet.Ruled.Uniformity = societyData.Ruled.Uniformities[Rand.Next(0, 4)];
//     planet.Ruled.MainConflict = societyData.Ruled.MainConflicts[Rand.Next(0, 12)];
//     planet.Ruled.Trends = societyData.Ruled.Trends[Rand.Next(0, 20)];
//
//     // Set the Planet's Flavor
//     planet.Flavor.BasicFlavor = societyData.Flavors.BasicFlavors[Rand.Next(0, 12)];
//     planet.Flavor.OutsiderTreatment = societyData.Flavors.OutsiderTreatments[Rand.Next(0, 6)];
//     planet.Flavor.PrimaryVirtue = societyData.Flavors.PrimaryVirtues[Rand.Next(0, 8)];
//     planet.Flavor.PrimaryVice = societyData.Flavors.PrimaryVices[Rand.Next(0, 10)];
//     planet.Flavor.XenophiliaDegree = societyData.Flavors.XenophiliaDegrees[Rand.Next(0, 4)];
//     planet.Flavor.PossiblePatron = societyData.Flavors.PossiblePatrons[Rand.Next(0, 12)];
//     planet.Flavor.Customs = societyData.Flavors.Customs[Rand.Next(0, 20)];
// }