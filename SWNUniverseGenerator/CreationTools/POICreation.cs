using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.DeserializedObjects;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.CreationTools
{
    internal class PoiCreation
    {
        private static readonly Random Rand = new Random();

        public Universe AddPoi(Universe universe, PoiDefaultSettings poiDefaultSettings, PoiData poiData)
        {
            universe.PointsOfInterest ??= new List<PointOfInterest>();

            var starId = string.IsNullOrEmpty(poiDefaultSettings.StarId)
                ? null
                : poiDefaultSettings.StarId;

            if (starId == null)
            {
                foreach (var star in universe.Stars)
                {
                    var max = poiDefaultSettings.PoiRange == null || poiDefaultSettings.PoiRange.Length == 0 ||
                              poiDefaultSettings.PoiRange[0] == -1 || poiDefaultSettings.PoiRange[1] == -1
                        ? Rand.Next(2, 5)
                        : Rand.Next(poiDefaultSettings.PoiRange[0], poiDefaultSettings.PoiRange[1] + 1);

                    var poiCount = 0;

                    while (poiCount < max)
                    {
                        var poi = new PointOfInterest();

                        // Generate a POI ID
                        IdGen.GenerateId(poi);

                        if (universe.PointsOfInterest.Exists(a => a.Id == poi.Id))
                            continue;

                        // Set the POI information with randomized data
                        poi.StarId = star.Id;
                        universe.Zones.Single(a => a.StarId == star.Id).PointsOfInterest.Add(poi.Id);
                        poi.Name = star.Name + " " + ToRoman(poiCount + 1);
                        var type = poiData.PointsOfInterest[Rand.Next(0, poiData.PointsOfInterest.Count)];
                        poi.Type = type.Type;
                        poi.OccupiedBy = type.OccupiedBy[Rand.Next(0, type.OccupiedBy.Count)];
                        poi.Situation = type.Situation[Rand.Next(0, type.Situation.Count)];

                        universe.PointsOfInterest.Add(poi);

                        poiCount++;
                    }
                }
            }
            else
            {
                var locId = (from stars in universe.Stars select new {ID = stars.Id})
                    .Single(a => a.ID == starId).ID;

                if (string.IsNullOrEmpty(locId))
                    throw new FileNotFoundException("No locations with ID " + starId + " found");

                var max = poiDefaultSettings.PoiRange == null || poiDefaultSettings.PoiRange.Length == 0 ||
                          poiDefaultSettings.PoiRange[0] == -1 || poiDefaultSettings.PoiRange[1] == -1
                    ? Rand.Next(1, 5)
                    : Rand.Next(poiDefaultSettings.PoiRange[0], poiDefaultSettings.PoiRange[1] + 1);

                var poiCount = 0;

                while (poiCount < max)
                {
                    var poi = new PointOfInterest();

                    // Generate a POI ID
                    IdGen.GenerateId(poi);

                    if (universe.PointsOfInterest.Exists(a => a.Id == poi.Id))
                        continue;

                    // Set the POI information with randomized data
                    poi.StarId = starId;
                    universe.Zones.Single(a => a.StarId == starId).PointsOfInterest.Add(poi.Id);
                    poi.Name = universe.Stars.Single(a => a.Id == starId).Name + " " + ToRoman(poiCount + 1);
                    var type = poiData.PointsOfInterest[Rand.Next(0, poiData.PointsOfInterest.Count)];
                    poi.Type = type.Type;
                    poi.OccupiedBy = type.OccupiedBy[Rand.Next(0, type.OccupiedBy.Count)];
                    poi.Situation = type.Situation[Rand.Next(0, type.Situation.Count)];

                    universe.PointsOfInterest.Add(poi);

                    poiCount++;
                }
            }

            return universe;
        }

        private static string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("Number not in range");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException("Number not in range");
        }
    }
}