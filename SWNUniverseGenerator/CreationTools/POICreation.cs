using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibGit2Sharp;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.CreationTools
{
    internal class PoiCreation
    {
        private static readonly Random Rand = new Random();

        public bool AddPoi(String universeId, PoiDefaultSettings poiDefaultSettings)
        {
            using var context = new UniverseContext();
            using var poiRepo = new Repository<PointOfInterest>(context);
            using var starRepo = new Repository<Star>(context);

            List<PointOfInterest> pointOfInterestList = new List<PointOfInterest>();

            // Create on all stars if the list is null
            if (poiDefaultSettings.LocationId == null)
            {
                List<Star> stars = starRepo
                    .Search(s => s.UniverseId == universeId)
                    .Cast<Star>()
                    .ToList();

                foreach (Star star in stars)
                {
                    // pointOfInterestList.AddRange(AddPointOfInterest(star, poiDefaultSettings, poiRepo));
                }
            }
            // Create on only the stars that are listed
            else
            {
                List<Star> stars = starRepo
                    .Search(s => s.UniverseId == universeId
                                 && poiDefaultSettings.LocationId.Contains(s.Id))
                    .Cast<Star>()
                    .ToList();

                foreach (Star star in stars)
                {
                    // pointOfInterestList.AddRange(AddPointOfInterest(star, poiDefaultSettings, poiRepo));
                }
            }


            return true;
        }

        // private List<PointOfInterest> AddPointOfInterest(Star star, PoiDefaultSettings poiDefaultSettings, Repository<PointOfInterest> poiRepo)
        // {
        //     PointOfInterest pointOfInterest;
        //
        //     var max = poiDefaultSettings.PoiRange == null || poiDefaultSettings.PoiRange.Length == 0 ||
        //               poiDefaultSettings.PoiRange[0] == -1 || poiDefaultSettings.PoiRange[1] == -1
        //         ? Rand.Next(1, 5)
        //         : Rand.Next(poiDefaultSettings.PoiRange[0], poiDefaultSettings.PoiRange[1] + 1);
        //
        //     var poiCount = 0;
        //
        //     while (poiCount < max)
        //     {
        //         var poi = new PointOfInterest();
        //
        //         // Set the POI information with randomized data
        //         poi.ZoneId = starId;
        //         poi.Name = star.Name + " " + ToRoman(poiCount + 1);
        //         poi.Type = poiRepo.Random(p => p.UniverseId);
        //         var type = poiData.PointsOfInterest[Rand.Next(0, poiData.PointsOfInterest.Count)];
        //         poi.Type = type.Type;
        //         poi.OccupiedBy = type.OccupiedBy[Rand.Next(0, type.OccupiedBy.Count)];
        //         poi.Situation = type.Situation[Rand.Next(0, type.Situation.Count)];
        //
        //         universe.PointsOfInterest.Add(poi);
        //
        //         poiCount++;
        //     }
        //
        //     return pointOfInterest;
        // }

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