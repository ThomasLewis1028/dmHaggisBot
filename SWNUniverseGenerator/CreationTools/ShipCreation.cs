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
                var ship = new Ship();

                IDGen.GenerateID(ship);

                if (universe.Ships.Exists(a => a.ID == ship.ID))
                    continue;

                var hullSwitch = Rand.Next(0, 100);

                // Weighted chances for each hull type
                switch (hullSwitch)
                {
                    case { } n when (n >= 0 && n < 10):
                        ship.Hull = shipData.Hulls[0];
                        break;
                    case { } n when (n >= 10 && n < 25):
                        ship.Hull = shipData.Hulls[1];
                        break;
                    case { } n when (n >= 25 && n < 50):
                        ship.Hull = shipData.Hulls[2];
                        break;
                    case { } n when (n >= 50 && n < 60):
                        ship.Hull = shipData.Hulls[3];
                        break;
                    case { } n when (n >= 60 && n < 72):
                        ship.Hull = shipData.Hulls[4];
                        break;
                    case { } n when (n >= 72 && n < 81):
                        ship.Hull = shipData.Hulls[5];
                        break;
                    case { } n when (n >= 81 && n < 86):
                        ship.Hull = shipData.Hulls[6];
                        break;
                    case { } n when (n >= 86 && n < 91):
                        ship.Hull = shipData.Hulls[7];
                        break;
                    case { } n when (n >= 91 && n < 94):
                        ship.Hull = shipData.Hulls[8];
                        break;
                    case { } n when (n >= 94 && n < 95):
                        ship.Hull = shipData.Hulls[9];
                        break;
                    case { } n when (n >= 95 && n < 97):
                        ship.Hull = shipData.Hulls[10];
                        break;
                    case { } n when (n >= 97 && n < 100):
                        ship.Hull = shipData.Hulls[11];
                        break;
                }

                CharCreation charCreation = new CharCreation();
                universe = charCreation.AddCharacters(universe,
                    new CharacterDefaultSettings
                        {Count = Rand.Next(ship.Hull.CrewMin, ship.Hull.CrewMax + 1), ShipID = ship.ID});

                var crewList = (from c in universe.Characters where c.ShipID == ship.ID select c.ID).ToList();

                if (ship.Hull.Type == "Strike Fighter")
                    ship.CaptainID = ship.PilotID = ship.EngineerID = ship.CommsID = ship.GunnerID = crewList[0];
                else
                {
                    ship.CaptainID = string.IsNullOrEmpty(shipDefaultSettings.CaptainID)
                        ? string.IsNullOrEmpty(crewList[0]) ? null : crewList[0]
                        : shipDefaultSettings.CaptainID;
                    ship.PilotID = string.IsNullOrEmpty(shipDefaultSettings.PilotID)
                        ? crewList.Count < 2
                            ? null
                            : crewList[1]
                        : shipDefaultSettings.PilotID;
                    ship.GunnerID = string.IsNullOrEmpty(shipDefaultSettings.GunnerID)
                        ? crewList.Count < 3
                            ? null
                            : crewList[2]
                        : shipDefaultSettings.GunnerID;
                    ship.EngineerID = string.IsNullOrEmpty(shipDefaultSettings.EngineerID)
                        ? crewList.Count < 4
                            ? null
                            : crewList[3]
                        : shipDefaultSettings.EngineerID;
                    ship.CommsID = string.IsNullOrEmpty(shipDefaultSettings.CommsID)
                        ? crewList.Count < 5
                            ? null
                            : crewList[4]
                        : shipDefaultSettings.CommsID;
                }

                universe.Ships.Add(ship);

                sCount++;
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