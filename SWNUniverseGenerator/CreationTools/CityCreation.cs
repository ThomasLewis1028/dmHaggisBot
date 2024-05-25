using System;
using System.Collections.Generic;
using System.Linq;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;
using Tag = SWNUniverseGenerator.Models.Tag;

namespace SWNUniverseGenerator.CreationTools;

/// <summary>
/// This class holds all the necessary functions for creating Cities and adding them to the Universe
/// </summary>
public class CityCreation
{
    private static readonly Random Rand = new();

    public bool AddCities(CityDefaultSettings cityDefaultSettings)
    {
        using var context = new UniverseContext();

        if (cityDefaultSettings.PlanetList != null)
        {
            if (cityDefaultSettings.PlanetList.Count > 1 && !string.IsNullOrEmpty(cityDefaultSettings.Name))
                throw new Exception("Cannot combine list of Planets and named Cities");
        }

        if (cityDefaultSettings.PlanetList == null && cityDefaultSettings.Population > 0)
        {
            throw new Exception("Cannot provide Population without specifying a planet");
        }
        
        if (cityDefaultSettings.PlanetList == null)
        {
            using (var planetRepo = new Repository<Planet>(context))
            {
                cityDefaultSettings.PlanetList = planetRepo.Search(e => e.UniverseId == cityDefaultSettings.UniverseId)
                    .ToList().Cast<Planet>().ToList();
            }
        }

        using (var cityRepo = new Repository<City>(context))
        {
            List<City> cities = new();

            foreach (var planet in cityDefaultSettings.PlanetList)
            {
                var popLeft = planet.Population;

                while (popLeft > 0)
                {
                    if (popLeft <= 10000)
                    {
                        CreateCity(cityDefaultSettings, context, cityRepo, planet, popLeft, cities);
                        popLeft = 0;
                    }
                    else
                    {
                        var percentage = Rand.Next(10,100) * .01;
                        var popTaken = (long)(popLeft * percentage);
                        CreateCity(cityDefaultSettings, context, cityRepo, planet, popTaken, cities);
                        popLeft -= popTaken;
                    }
                }

                cityRepo.AddRange(cities);
                cities.Clear();
            }
        }

        return true;
    }

    public static bool CreateCity(CityDefaultSettings cityDefaultSettings, UniverseContext context,
        Repository<City> cityRepo, Planet planet, long population, List<City> cities)
    {
        City city = new City
        {
            Population = population,
            PlanetId = planet.Id,
            UniverseId = cityDefaultSettings.UniverseId
        };
        
        while (true)
        {
            // Pick a random name out of the list of cities
            using (var repo = new Repository<Naming>(context))
            {
                city.Name = string.IsNullOrEmpty(cityDefaultSettings.Name)
                    ? "New " + ((Naming)repo.Random(n => n.NameType == "City")).Name
                    : cityDefaultSettings.Name;
            }

            // No cities can share a name
            if (!cityRepo.Any(a => a.UniverseId == cityDefaultSettings.UniverseId && a.Name == city.Name))
                break;
        }
        
        cities.Add(city);

        return true;
    }
}