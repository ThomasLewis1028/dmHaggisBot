using System;
using System.Collections.Generic;
using System.Linq;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

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
        /// <returns>
        /// The newly updated Universe
        /// </returns>
        public bool AddPlanets(String universeId, PlanetDefaultSettings planetDefaultSettings)
        {
            if (planetDefaultSettings.StarList == null)
                throw new Exception("StarList cannot be null");

            if (planetDefaultSettings.StarList != null)
                if (planetDefaultSettings.StarList.Count > 1 && !string.IsNullOrEmpty(planetDefaultSettings.Name))
                    throw new Exception("Cannot combine list of Stars and Named Planets");

            using (var context = new UniverseContext())
            {
                using (var planetRepo = new Repository<Planet>(context))
                {
                    List<Planet> planets = new();

                    // Iterate through each star and add planets
                    foreach (var entity in planetDefaultSettings.StarList)
                    {
                        var star = (Star)entity;
                        int pMax;

                        // Set the random number of Planets that will be created for a given Star 
                        if (string.IsNullOrEmpty(planetDefaultSettings.Name))
                        {
                            pMax = Rand.Next(planetDefaultSettings.PlanetRange.Item1,
                                planetDefaultSettings.PlanetRange.Item2 + 1);

                            if (pMax == 0)
                                pMax = Rand.NextDouble() < 0.75
                                    ? 1
                                    : 0;
                        }
                        // If there is a specified name, only allow one Planet to be created
                        else
                            pMax = 1;

                        var pCount = 0;

                        while (pCount < pMax)
                            pCount = CreatePlanet(universeId, planetDefaultSettings, context, planetRepo, star, pCount,
                                planets);

                        planetRepo.AddRange(planets);
                        planets.Clear();
                    }
                }
            }

            return true;
        }

        private static int CreatePlanet(string universeId, PlanetDefaultSettings planetDefaultSettings,
            UniverseContext context,
            Repository<Planet> planetRepo, Star star, int pCount, List<Planet> planets)
        {
            var planet = new Planet
            {
                UniverseId = universeId
                // Society = new Society(), Ruler = new Ruler(), Ruled = new Ruled(), Flavor = new Flavor()
            };

            // Name the Planet
            while (true)
            {
                // Pick a random name out of the list of Planets
                using (var repo = new Repository<Naming>(context))
                {
                    planet.Name = string.IsNullOrEmpty(planetDefaultSettings.Name)
                        ? ((Naming)repo.Random(n => n.NameType == "Planet")).Name
                        : planetDefaultSettings.Name;
                }

                // No planets can share a name
                if (!planetRepo.Any(a => a.UniverseId == universeId && a.Name == planet.Name))
                    break;
            }

            // Set the Planet information from either a randomized value or specified information
            planet.ZoneId = star.ZoneId;

            using (var repo = new Repository<Tag>(context))
            {
                planet.FirstWorldTag = repo.Random().Id;

                while (string.IsNullOrEmpty(planet.SecondWorldTag) ||
                       planet.SecondWorldTag == planet.FirstWorldTag)
                {
                    planet.SecondWorldTag = repo.Random().Id;
                }
            }

            using (var repo = new Repository<Atmosphere>(context))
                planet.Atmosphere = repo.Random().Id;

            using (var repo = new Repository<Temperature>(context))
                planet.Temperature = repo.Random().Id;

            using (var repo = new Repository<Biosphere>(context))
                planet.Biosphere = repo.Random().Id;

            using (var repo = new Repository<Population>(context))
                planet.Population = repo.Random().Id;

            using (var repo = new Repository<TechLevel>(context))
                planet.TechLevel = repo.Random().Id;

            // Set primary world
            if (pCount == 0)
                planet.IsPrimary = true;
            else
            {
                // Non-primary world information
                // planet.Origin = worldInfo.OwOrigins[Rand.Next(0, 8)];
                // planet.Relationship = worldInfo.OwRelationships[Rand.Next(0, 8)];
                // planet.Contact = worldInfo.OwContacts[Rand.Next(0, 8)];
                planet.IsPrimary = false;
            }

            // Add the Planet to the current Universe
            planets.Add(planet);
            pCount++;
            return pCount;
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