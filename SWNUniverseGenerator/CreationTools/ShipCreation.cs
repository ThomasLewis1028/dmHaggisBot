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

        public Universe AddShips(Universe universe, ShipDefaultSettings shipDefaultSettings, ShipData shipData, CharData charData)
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

                Presets presets = shipData.Presets.Find(a => a.HullType == ship.Hull.Type);
                Preset preset = presets.ListPresets[Rand.Next(0, presets.ListPresets.Count)];
                ship.CrewSkill = preset.CrewSkill;
                ship.Cp = preset.Cp;
                if (preset.Weapons != null)
                {
                    ship.Weapons = new List<Weapon>();
                    foreach (var w in preset.Weapons)
                        ship.Weapons.Add(shipData.Weapons[w]);
                }

                if (preset.Defenses != null)
                {
                    ship.Defenses = new List<Defense>();
                    foreach (var d in preset.Defenses)
                        ship.Defenses.Add(shipData.Defenses[d]);
                }

                if (preset.Fittings != null)
                {
                    ship.Fittings = new List<Fitting>();
                    foreach (var f in preset.Fittings)
                        ship.Fittings.Add(shipData.Fittings[f]);
                }

                CharCreation charCreation = new CharCreation();
                universe = charCreation.AddCharacters(universe,
                    new CharacterDefaultSettings
                        {Count = Rand.Next(ship.Hull.CrewMin, ship.Hull.CrewMax + 1), ShipId = ship.Id}, charData);

                var crewList = (from c in universe.Characters where c.ShipId == ship.Id select c.Id).ToList();

                if (ship.Hull.Type == "Strike Fighter")
                    ship.CaptainId = ship.PilotId = ship.EngineerId = ship.CommsId = ship.GunnerId = crewList[0];
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

                universe.Ships.Add(ship);

                sCount++;
            }

            // Re-order the list of Ships by their ID
            universe.Ships = universe.Ships.OrderBy(s => s.Id).ToList();

            return universe;
        }
    }
}