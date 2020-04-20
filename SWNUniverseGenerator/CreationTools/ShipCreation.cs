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
    public class ShipCreation
    {
        private static readonly Random Rand = new Random();
        
        public Universe AddShips(Universe universe, ShipDefaultSettings shipDefaultSettings)
        {
            // Create a list of ships if it doesn't already exist
            universe.Ships ??= new List<Ship>();
            
            // Deserialize data
            var shipData = LoadShipData();
            
            // Set the number of ships you want to create. Default is 1
            var count = shipDefaultSettings.Count < 0
                ? 1
                : shipDefaultSettings.Count;

            var sCount = 0;
            while (sCount < count)
            {
                
            }
            
            // Re-order the list of Ships by their ID
            universe.Ships = universe.Ships.OrderBy(s => s.ID).ToList();

            return universe;
        }
        
        private ShipData LoadShipData()
        {
            var shipData =
                JObject.Parse(
                    File.ReadAllText(@"Data/shipData.json"));

            return JsonConvert.DeserializeObject<ShipData>(shipData.ToString());
        }
    }
}