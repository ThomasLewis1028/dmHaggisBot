using System;
using System.Collections.Generic;
using System.Linq;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;
using Tag = SWNUniverseGenerator.Models.Tag;

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
        public bool AddPlanets(PlanetDefaultSettings planetDefaultSettings)
        {
            using var context = new UniverseContext();

            if (planetDefaultSettings.StarList == null)
            {
                using (var starRepo = new Repository<Star>(context))
                {
                    planetDefaultSettings.StarList =
                        starRepo.Search(e => e.UniverseId == planetDefaultSettings.UniverseId).ToList().Cast<Star>()
                            .ToList();
                }
            }

            if (planetDefaultSettings.StarList != null)
                if (planetDefaultSettings.StarList.Count > 1 && !string.IsNullOrEmpty(planetDefaultSettings.Name))
                    throw new Exception("Cannot combine list of Stars and Named Planets");

            using (var planetRepo = new Repository<Planet>(context))
            {
                List<Planet> planets = new();

                // Iterate through each star and add planets
                foreach (var star in planetDefaultSettings.StarList)
                {
                    int pMax = 1;

                    pMax += Rand.NextDouble() < 0.05
                        ? Rand.NextDouble() < 0.5 
                            ? 1 
                            : -1
                        : 0;

                    var pCount = 0;

                    while (pCount < pMax)
                        pCount = CreatePlanet(planetDefaultSettings, context, planetRepo, star, pCount,
                            planets);

                    planetRepo.AddRange(planets);
                    planets.Clear();
                }
            }

            return true;
        }

        private static int CreatePlanet(PlanetDefaultSettings planetDefaultSettings,
            UniverseContext context,
            Repository<Planet> planetRepo, Star star, int pCount, List<Planet> planets)
        {
            var planet = new Planet
            {
                UniverseId = planetDefaultSettings.UniverseId
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
                if (!planetRepo.Any(a => a.UniverseId == planetDefaultSettings.UniverseId && a.Name == planet.Name))
                    break;
            }

            // Set the Planet information from either a randomized value or specified information
            planet.ZoneId = star.ZoneId;

            bool barren = false;

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
            {
                var popRoll = Rand.Next(1, 7) + Rand.Next(1, 7);

                Population pop = repo.Search(p => p.MinRoll <= popRoll && p.MaxRoll >= popRoll).Cast<Population>().First();
                
                planet.Population = Rand.NextInt64(pop.MinPop, pop.MaxPop);
            }

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