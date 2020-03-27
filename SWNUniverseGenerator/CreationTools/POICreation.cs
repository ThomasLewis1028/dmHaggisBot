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
                        
                        if(universe.PointsOfInterest.Exists(a => a.ID == poi.ID))
                            continue;

                        // Set the POI information with randomized data
                        poi.StarID = star.ID;
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
    }
}