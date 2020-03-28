using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.DeserializedObjects;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.CreationTools
{
    public class POICreation
    {
        private static readonly Random Rand = new Random();

        public Universe AddPOI(Universe universe, POIDefaultSettings poiDefaultSettings)
        {
            if (universe.PointsOfInterest == null)
                universe.PointsOfInterest = new List<PointOfInterest>();

            // Deserialize data
            var poiData = LoadPOIData();

            var starID = string.IsNullOrEmpty(poiDefaultSettings.StarID)
                ? null
                : poiDefaultSettings.StarID;

            if (starID == null)
            {
                foreach (var star in universe.Stars)
                {
                    var max = poiDefaultSettings.POIRange == null || poiDefaultSettings.POIRange.Length == 0 ||
                              poiDefaultSettings.POIRange[0] == -1 || poiDefaultSettings.POIRange[1] == -1
                        ? Rand.Next(2, 5)
                        : Rand.Next(poiDefaultSettings.POIRange[0], poiDefaultSettings.POIRange[1] + 1);

                    var poiCount = 0;

                    while (poiCount < max)
                    {
                        var poi = new PointOfInterest();

                        // Generate a POI ID
                        IDGen.GenerateID(poi);

                        if (universe.PointsOfInterest.Exists(a => a.ID == poi.ID))
                            continue;

                        // Set the POI information with randomized data
                        poi.StarID = star.ID;
                        poi.Name = star.Name + " " + ToRoman(poiCount);
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
                var locID = (from stars in universe.Stars select new {stars.ID})
                    .Single(a => a.ID == starID).ID;

                if (string.IsNullOrEmpty(locID))
                    throw new FileNotFoundException("No locations with ID " + starID + " found");

                var max = poiDefaultSettings.POIRange == null || poiDefaultSettings.POIRange.Length == 0 ||
                          poiDefaultSettings.POIRange[0] == -1 || poiDefaultSettings.POIRange[1] == -1
                    ? Rand.Next(2, 5)
                    : Rand.Next(poiDefaultSettings.POIRange[0], poiDefaultSettings.POIRange[1] + 1);

                var poiCount = 0;

                while (poiCount < max)
                {
                    var poi = new PointOfInterest();

                    // Generate a POI ID
                    IDGen.GenerateID(poi);

                    if (universe.PointsOfInterest.Exists(a => a.ID == poi.ID))
                        continue;

                    // Set the POI information with randomized data
                    poi.StarID = starID;
                    poi.Name = universe.Stars.Single(a => a.ID == starID).Name + " " + ToRoman(poiCount);
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

        private POIData LoadPOIData()
        {
            var poiData =
                JObject.Parse(
                    File.ReadAllText(@"Data/pointsOfInterest.json"));

            return JsonConvert.DeserializeObject<POIData>(poiData.ToString());
        }


        public static string ToRoman(int number)
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