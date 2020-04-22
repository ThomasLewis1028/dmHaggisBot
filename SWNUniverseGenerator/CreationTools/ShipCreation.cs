using System;
using System.Collections.Generic;
using System.Linq;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.DeserializedObjects;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.CreationTools
{
    internal class ShipCreation
    {
        private static readonly Random Rand = new Random();

        public Universe AddShips(Universe universe, ShipDefaultSettings shipDefaultSettings, ShipData shipData,
            CharData charData)
        {
            // Create a list of ships if it doesn't already exist
            universe.Ships ??= new List<Ship>();

            // Set the number of ships you want to create. Default is 1
            var count = shipDefaultSettings.Count < 0
                ? 1
                : shipDefaultSettings.Count;

            var sCount = 0;
            while (sCount < count)
            {
                var ship = new Ship();

                IdGen.GenerateId(ship);

                if (universe.Ships.Exists(a => a.Id == ship.Id))
                    continue;

                var hullSwitch = Rand.Next(0, 100);

                Hull hull;

                // Weighted chances for each hull type
                switch (hullSwitch)
                {
                    case { } n when (n >= 0 && n < 10):
                        hull = shipData.Hulls[0];
                        break;
                    case { } n when (n >= 10 && n < 25):
                        hull = shipData.Hulls[1];
                        break;
                    case { } n when (n >= 25 && n < 50):
                        hull = shipData.Hulls[2];
                        break;
                    case { } n when (n >= 50 && n < 60):
                        hull = shipData.Hulls[3];
                        break;
                    case { } n when (n >= 60 && n < 72):
                        hull = shipData.Hulls[4];
                        break;
                    case { } n when (n >= 72 && n < 81):
                        hull = shipData.Hulls[5];
                        break;
                    case { } n when (n >= 81 && n < 86):
                        hull = shipData.Hulls[6];
                        break;
                    case { } n when (n >= 86 && n < 91):
                        hull = shipData.Hulls[7];
                        break;
                    case { } n when (n >= 91 && n < 94):
                        hull = shipData.Hulls[8];
                        break;
                    case { } n when (n >= 94 && n < 95):
                        hull = shipData.Hulls[9];
                        break;
                    case { } n when (n >= 95 && n < 97):
                        hull = shipData.Hulls[2]; // Free Merchant temporary value
                        // hull = shipData.Hulls[10];
                        break;
                    case { } n when (n >= 97 && n < 100):
                        hull = shipData.Hulls[2]; // Free Merchant temporary value
                        // hull = shipData.Hulls[11];
                        break;
                    default:
                        hull = shipData.Hulls[2];
                        break;
                }

                ship.Hull = hull.Type;

                Presets presets = shipData.Presets.Find(a => a.HullType == hull.Type);
                Preset preset = presets.ListPresets[Rand.Next(0, presets.ListPresets.Count)];
                ship.CrewSkill = preset.CrewSkill;
                ship.Cp = preset.Cp;
                if (preset.Weapons != null)
                {
                    ship.Weapons = new List<String>();
                    foreach (var w in preset.Weapons)
                        ship.Weapons.Add(shipData.Weapons[w].Type);
                }

                if (preset.Defenses != null)
                {
                    ship.Defenses = new List<String>();
                    foreach (var d in preset.Defenses)
                        ship.Defenses.Add(shipData.Defenses[d].Type);
                }

                if (preset.Fittings != null)
                {
                    ship.Fittings = new List<String>();
                    foreach (var f in preset.Fittings)
                        ship.Fittings.Add(shipData.Fittings[f].Type);
                }

                CharCreation charCreation = new CharCreation();
                universe = charCreation.AddCharacters(universe,
                    new CharacterDefaultSettings
                        {Count = Rand.Next(hull.CrewMin, hull.CrewMax + 1), ShipId = ship.Id}, charData);

                var crewList = (from c in universe.Characters where c.ShipId == ship.Id select c.Id).ToList();

                if (hull.Type == "Strike Fighter")
                    ship.CaptainId = crewList[0];
                else
                {
                    ship.CaptainId = string.IsNullOrEmpty(shipDefaultSettings.CaptainId)
                        ? string.IsNullOrEmpty(crewList[0]) ? null : crewList[0]
                        : shipDefaultSettings.CaptainId;
                    ship.PilotId = string.IsNullOrEmpty(shipDefaultSettings.PilotId)
                        ? crewList.Count < 2
                            ? null
                            : crewList[1]
                        : shipDefaultSettings.PilotId;
                    ship.GunnerId = string.IsNullOrEmpty(shipDefaultSettings.GunnerId)
                        ? crewList.Count < 3
                            ? null
                            : crewList[2]
                        : shipDefaultSettings.GunnerId;
                    ship.EngineerId = string.IsNullOrEmpty(shipDefaultSettings.EngineerId)
                        ? crewList.Count < 4
                            ? null
                            : crewList[3]
                        : shipDefaultSettings.EngineerId;
                    ship.CommsId = string.IsNullOrEmpty(shipDefaultSettings.CommsId)
                        ? crewList.Count < 5
                            ? null
                            : crewList[4]
                        : shipDefaultSettings.CommsId;
                }

                if (ship.CaptainId != null)
                    universe.Characters.Find(c => c.Id == ship.CaptainId).Title = "Captain";
                if (ship.PilotId != null)
                    universe.Characters.Find(c => c.Id == ship.PilotId).Title = "Pilot";
                if (ship.GunnerId != null)
                    universe.Characters.Find(c => c.Id == ship.GunnerId).Title = "Gunner";
                if (ship.EngineerId != null)
                    universe.Characters.Find(c => c.Id == ship.EngineerId).Title = "Engineer";
                if (ship.CommsId != null)
                    universe.Characters.Find(c => c.Id == ship.CommsId).Title = "Comms Expert";

                universe.Ships.Add(ship);

                sCount++;
            }

            // Re-order the list of Ships by their ID
            universe.Ships = universe.Ships.OrderBy(s => s.Id).ToList();

            return universe;
        }
    }
}