using System;
using System.Collections.Generic;
using System.Linq;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.CreationTools
{
    internal class PoiCreation
    {
        private static readonly Random Rand = new Random();

        public bool AddPoi(PoiDefaultSettings poiDefaultSettings)
        {
            using var context = new UniverseContext();
            using var poiRepo = new Repository<PointOfInterest>(context);
            using var starRepo = new Repository<Star>(context);
            using var planetRepo = new Repository<Planet>(context);

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
                    pointOfInterestList.AddRange(AddPointOfInterest(star, poiDefaultSettings, poiRepo, context));
                }
                
                List<Planet> planets = planetRepo
                    .Search(s => s.UniverseId == poiDefaultSettings.UniverseId)
                    .Cast<Planet>()
                    .ToList();

                foreach (Planet planet in planets)
                {
                    pointOfInterestList.AddRange(AddPointOfInterest(planet, poiDefaultSettings, poiRepo, context));
                }
            }
            // Create on only the stars/planets that are listed
            else
            {
                List<Star> stars = starRepo
                    .Search(s => s.UniverseId == poiDefaultSettings.UniverseId
                                 && poiDefaultSettings.LocationId.Contains(s.Id))
                    .Cast<Star>()
                    .ToList();

                foreach (Star star in stars)
                {
                    pointOfInterestList.AddRange(AddPointOfInterest(star, poiDefaultSettings, poiRepo, context));
                }
                
                List<Planet> planets = planetRepo
                    .Search(s => s.UniverseId == poiDefaultSettings.UniverseId
                                 && poiDefaultSettings.LocationId.Contains(s.Id))
                    .Cast<Planet>()
                    .ToList();

                foreach (Planet planet in planets)
                {
                    pointOfInterestList.AddRange(AddPointOfInterest(planet, poiDefaultSettings, poiRepo, context));
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
                    Name = star.Name + " " + RomanNumerals.ToRoman(poiCount + 1),
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
                    Name = planet.Name + " " + RomanNumerals.ToRoman(poiCount + 1),
                    Type = poiType.Type,
                    OccupiedBy = ((PoiOccupiedBy)poiOccupiedByRepo.Random(e => e.TypeId == poiType.Id)).OccupiedBy,
                    Situation = ((PoiSituation)poiSituationRepo.Random(e => e.TypeId == poiType.Id)).Situation
                };
                
                pointsOfInterest.Add(poi);
        
                poiCount++;
            }
        
            return pointsOfInterest;
        }
    }
}