using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.CreationTools
{
    internal class PoiCreation: ContextService<PoiCreation>
    {
        private static readonly Random Rand = new();

        public PoiCreation(UniverseContext context) : base(context)
        {

        }

        public bool AddPoi(PoiDefaultSettings poiDefaultSettings)
        {
            using var poiRepo = new Repository<PointOfInterest>(_context);
            using var starRepo = new Repository<Star>(_context);
            using var planetRepo = new Repository<Planet>(_context);

            List<PointOfInterest> pointOfInterestList = new List<PointOfInterest>();

            // Create on all stars if the list is null
            if (poiDefaultSettings.LocationId == null)
            {
                List<Star> stars = starRepo
                    .Search(s => s.UniverseId == poiDefaultSettings.UniverseId)
                    .Cast<Star>()
                    .ToList();

                foreach (Star star in stars)
                {
                    pointOfInterestList.AddRange(AddPointOfInterest(star, poiDefaultSettings, poiRepo, _context));
                }
                
                List<Planet> planets = planetRepo
                    .Search(s => s.UniverseId == poiDefaultSettings.UniverseId)
                    .Cast<Planet>()
                    .ToList();

                foreach (Planet planet in planets)
                {
                    pointOfInterestList.AddRange(AddPointOfInterest(planet, poiDefaultSettings, poiRepo, _context));
                }
            }
            // Create on only the stars that are listed
            else
            {
                List<Star> stars = starRepo
                    .Search(s => s.UniverseId == poiDefaultSettings.UniverseId
                                 && poiDefaultSettings.LocationId.Contains(s.Id))
                    .Cast<Star>()
                    .ToList();

                foreach (Star star in stars)
                {
                    pointOfInterestList.AddRange(AddPointOfInterest(star, poiDefaultSettings, poiRepo, _context));
                }
                
                List<Planet> planets = planetRepo
                    .Search(s => s.UniverseId == poiDefaultSettings.UniverseId
                                 && poiDefaultSettings.LocationId.Contains(s.Id))
                    .Cast<Planet>()
                    .ToList();

                foreach (Planet planet in planets)
                {
                    pointOfInterestList.AddRange(AddPointOfInterest(planet, poiDefaultSettings, poiRepo, _context));
                }
            }

            poiRepo.AddRange(pointOfInterestList);
            return true;
        }

        private List<PointOfInterest> AddPointOfInterest(Star star, PoiDefaultSettings poiDefaultSettings, Repository<PointOfInterest> poiRepo, UniverseContext context)
        {
            List<PointOfInterest> pointsOfInterest = new ();

            using var poiTypeRepo = new Repository<PoiType>(context);
            using var poiOccupiedByRepo = new Repository<PoiOccupiedBy>(context);
            using var poiSituationRepo = new Repository<PoiSituation>(context);
        
            var max = poiDefaultSettings.PoiRange == null || poiDefaultSettings.PoiRange.Length == 0 ||
                      poiDefaultSettings.PoiRange[0] == -1 || poiDefaultSettings.PoiRange[1] == -1
                ? Rand.Next(1, 3)
                : Rand.Next(poiDefaultSettings.PoiRange[0], poiDefaultSettings.PoiRange[1] + 1);
        
            var poiCount = 0;
        
            while (poiCount < max)
            {
                var poiType = (PoiType)poiTypeRepo.Random();
                var poi = new PointOfInterest
                {
                    // Set the POI information with randomized data
                    UniverseId = star.UniverseId,
                    LocationId = star.Id,
                    Name = star.Name + " " + ToRoman(poiCount + 1),
                    Type = poiType.Type,
                    OccupiedBy = ((PoiOccupiedBy)poiOccupiedByRepo.Random(e => e.TypeId == poiType.Id)).OccupiedBy,
                    Situation = ((PoiSituation)poiSituationRepo.Random(e => e.TypeId == poiType.Id)).Situation
                };
                
                pointsOfInterest.Add(poi);
        
                poiCount++;
            }
        
            return pointsOfInterest;
        }
        
        private List<PointOfInterest> AddPointOfInterest(Planet planet, PoiDefaultSettings poiDefaultSettings, Repository<PointOfInterest> poiRepo, UniverseContext context)
        {
            List<PointOfInterest> pointsOfInterest = new ();

            using var poiTypeRepo = new Repository<PoiType>(context);
            using var poiOccupiedByRepo = new Repository<PoiOccupiedBy>(context);
            using var poiSituationRepo = new Repository<PoiSituation>(context);
        
            var max = poiDefaultSettings.PoiRange == null || poiDefaultSettings.PoiRange.Length == 0 ||
                      poiDefaultSettings.PoiRange[0] == -1 || poiDefaultSettings.PoiRange[1] == -1
                ? Rand.Next(1, 3)
                : Rand.Next(poiDefaultSettings.PoiRange[0], poiDefaultSettings.PoiRange[1] + 1);
        
            var poiCount = 0;
        
            while (poiCount < max)
            {
                var poiType = (PoiType)poiTypeRepo.Random();
                var poi = new PointOfInterest
                {
                    // Set the POI information with randomized data
                    UniverseId = planet.UniverseId,
                    LocationId = planet.Id,
                    Name = planet.Name + " " + ToRoman(poiCount + 1),
                    Type = poiType.Type,
                    OccupiedBy = ((PoiOccupiedBy)poiOccupiedByRepo.Random(e => e.TypeId == poiType.Id)).OccupiedBy,
                    Situation = ((PoiSituation)poiSituationRepo.Random(e => e.TypeId == poiType.Id)).Situation
                };
                
                pointsOfInterest.Add(poi);
        
                poiCount++;
            }
        
            return pointsOfInterest;
        }

        private static string ToRoman(int number)
        {
            switch (number)
            {
                case < 0:
                case > 3999:
                    throw new ArgumentOutOfRangeException("Number not in range");
                case < 1:
                    return string.Empty;
                case >= 1000:
                    return "M" + ToRoman(number - 1000);
                case >= 900:
                    return "CM" + ToRoman(number - 900);
                case >= 500:
                    return "D" + ToRoman(number - 500);
                case >= 400:
                    return "CD" + ToRoman(number - 400);
                case >= 100:
                    return "C" + ToRoman(number - 100);
                case >= 90:
                    return "XC" + ToRoman(number - 90);
                case >= 50:
                    return "L" + ToRoman(number - 50);
                case >= 40:
                    return "XL" + ToRoman(number - 40);
                case >= 10:
                    return "X" + ToRoman(number - 10);
                case >= 9:
                    return "IX" + ToRoman(number - 9);
                case >= 5:
                    return "V" + ToRoman(number - 5);
                case >= 4:
                    return "IV" + ToRoman(number - 4);
                case >= 1:
                    return "I" + ToRoman(number - 1);
            }
        }
    }
}