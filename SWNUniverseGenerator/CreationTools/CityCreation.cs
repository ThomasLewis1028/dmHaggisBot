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

        if (cityDefaultSettings.PlanetList == null)
        {
            using (var planetRepo = new Repository<Planet>(context))
            {
                cityDefaultSettings.PlanetList = planetRepo.Search(e => e.UniverseId == cityDefaultSettings.UniverseId)
                    .ToList().Cast<Planet>().ToList();
            }
        }

        if (cityDefaultSettings.PlanetList != null)
        {
            if (cityDefaultSettings.PlanetList.Count > 1 && !string.IsNullOrEmpty(cityDefaultSettings.Name))
                throw new Exception("Cannot combine list of Planets and named Cities");
        }

        using (var cityRepo = new Repository<City>(context))
        {
            List<City> cities = new();
        }

        return true;
    }

    public static int CreateCity(CityDefaultSettings cityDefaultSettings, UniverseContext context,
        Repository<City> cityRepo, Planet planet, int cCount, List<City> cities)
    {
        return 1;
    }
}