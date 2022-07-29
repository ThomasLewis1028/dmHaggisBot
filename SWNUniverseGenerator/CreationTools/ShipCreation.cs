using System;
using System.Collections.Generic;
using System.IO;
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
            CharData charData, List<NameGeneration> nameGenerations)
        {
            // Create a list of ships if it doesn't already exist
            universe.Ships ??= new List<Ship>();

            // Set the number of ships you want to create. Default is 1
            var count = shipDefaultSettings.Count < 0
                ? 1
                : shipDefaultSettings.Count;

            var homeId = string.IsNullOrEmpty(shipDefaultSettings.HomeId)
                ? null
                : shipDefaultSettings.HomeId;

            var locId = string.IsNullOrEmpty(shipDefaultSettings.LocationId)
                ? null
                : shipDefaultSettings.LocationId;

            var type = string.IsNullOrEmpty(shipDefaultSettings.Type)
                ? null
                : shipDefaultSettings.Type;

            var sCount = 0;
            while (sCount < count)
            {
                var ship = new Ship();

                IdGen.GenerateId(ship);

                if (universe.Ships.Exists(a => a.Id == ship.Id))
                    continue;

                Hull hull;

                // Set the type of ship
                if (string.IsNullOrEmpty(type))
                {
                    var hullSwitch = Rand.Next(0, 100);


                    // Weighted chances for each hull type
                    hull = hullSwitch switch
                    {
                        { } n when (n >= 0 && n < 10) => shipData.Hulls[0],
                        { } n when (n >= 10 && n < 25) => shipData.Hulls[1],
                        { } n when (n >= 25 && n < 50) => shipData.Hulls[2],
                        { } n when (n >= 50 && n < 60) => shipData.Hulls[3],
                        { } n when (n >= 60 && n < 72) => shipData.Hulls[4],
                        { } n when (n >= 72 && n < 81) => shipData.Hulls[5],
                        { } n when (n >= 81 && n < 86) => shipData.Hulls[6],
                        { } n when (n >= 86 && n < 91) => shipData.Hulls[7],
                        { } n when (n >= 91 && n < 94) => shipData.Hulls[8],
                        { } n when (n >= 94 && n < 95) => shipData.Hulls[9],
                        { } n when (n >= 95 && n < 97) => shipData.Hulls[2], // Free Merchant temporary value
                        { } n when (n >= 97 && n < 100) => shipData.Hulls[2], // Free Merchant temporary value
                        _ => shipData.Hulls[2]
                    };
                }
                else if (shipData.Hulls.Exists(h => h.Type == type))
                    hull = shipData.Hulls.Find(h => h.Type == type);
                else
                    throw new FileLoadException("No ship of this type exists");

                if (string.IsNullOrEmpty(shipDefaultSettings.Name))
                {
                    while (true)
                    {
                        var name = "";
                        var nameType = Rand.Next(0, 100);

                        if (nameType > 0)
                        {
                            name += shipData.Naming.Adjectives[Rand.Next(0, shipData.Naming.Adjectives.Count)];

                            name += Rand.Next(0, 10) == 0 
                                ? "-" + shipData.Naming.Adjectives[Rand.Next(0, shipData.Naming.Adjectives.Count)] 
                                : "";
                            
                            nameType = Rand.Next(0, 2);

                            if (nameType == 0)
                                name += " " +
                                        shipData.Naming.Animals[Rand.Next(0, shipData.Naming.Animals.Count)];
                            else
                                name += " " +
                                        shipData.Naming.Nouns[Rand.Next(0, shipData.Naming.Nouns.Count)];
                        }
                        else
                            name += shipData.Naming.Nouns[Rand.Next(0, shipData.Naming.Nouns.Count)];

                        ship.Name = name;

                        if (!universe.Ships.Exists(a => a.Name == name))
                            break;
                    }
                }

                ship.Hull = hull.Type;
                ship.HomeId = homeId;
                ship.LocationId = locId;

                Presets presets = shipData.Presets.Find(a => a.HullType == hull.Type);
                Preset preset = presets.ListPresets[Rand.Next(0, presets.ListPresets.Count)];
                ship.CrewSkill = preset.CrewSkill;
                ship.Cp = preset.Cp;

                // Set the weapons
                if (preset.Weapons != null)
                {
                    ship.Weapons = new List<String>();
                    foreach (var w in preset.Weapons)
                        ship.Weapons.Add(shipData.Weapons[w].Type);
                }

                // Set the defenses
                if (preset.Defenses != null)
                {
                    ship.Defenses = new List<String>();
                    foreach (var d in preset.Defenses)
                        ship.Defenses.Add(shipData.Defenses[d].Type);
                }

                // Set the fittings
                if (preset.Fittings != null)
                {
                    ship.Fittings = new List<String>();
                    foreach (var f in preset.Fittings)
                        ship.Fittings.Add(shipData.Fittings[f].Type);
                }

                // Set the crew if the CrewId is not null
                if (shipDefaultSettings.CrewId != null)
                    foreach (var c in shipDefaultSettings.CrewId)
                        universe.Characters.Find(a => a.Id == c).ShipId = ship.Id;


                // Create the crew for the ship
                if (shipDefaultSettings.CreateCrew)
                {
                    CharCreation charCreation = new CharCreation();
                    universe = charCreation.AddCharacters(universe,
                        new CharacterDefaultSettings
                        {
                            Count = Rand.Next(hull.CrewMin,
                                hull.CrewMax + 1 - (shipDefaultSettings.CrewId?.Count ?? 0)),
                            ShipId = ship.Id
                        }, charData, nameGenerations);

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

                    // Tie the main crew to the ship
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
                }

                // If the ship has the Ship Bay/Fighter fitting, generate a ship to go in that space
                if (ship.Fittings.Contains("Ship bay/fighter"))
                {
                    var fighterHangarCount = ship.Fittings.FindAll(f => f.Contains("Ship bay/fighter")).Count;

                    ShipCreation shipCreation = new ShipCreation();

                    var randFhc = Rand.Next(0, fighterHangarCount);
                    var fCount = 0;
                    while (fCount < randFhc)
                    {
                        universe = shipCreation.AddShips(universe,
                            new ShipDefaultSettings
                            {
                                Count = 1,
                                CreateCrew = false,
                                HomeId = ship.Id,
                                Type = shipData.Hulls.FindAll(h => h.Class == "Fighter")[
                                    Rand.Next(0, shipData.Hulls.FindAll(h => h.Class == "Fighter").Count)].Type
                            },
                            shipData, charData, nameGenerations);
                        fCount++;
                    }
                }

                // If the ship has the Ship Bay/Frigate fitting, generate a ship to go in that space
                if (ship.Fittings.Contains("Ship bay/frigate"))
                {
                    var frigateHangarCount = ship.Fittings.FindAll(f => f.Contains("Ship bay/frigate")).Count;

                    ShipCreation shipCreation = new ShipCreation();

                    var randFhc = Rand.Next(0, frigateHangarCount);
                    var fCount = 0;
                    while (fCount < randFhc)
                    {
                        universe = shipCreation.AddShips(universe,
                            new ShipDefaultSettings
                            {
                                Count = 1,
                                CreateCrew = false,
                                HomeId = ship.Id,
                                Type = shipData.Hulls.FindAll(h => h.Class == "Frigate")[
                                    Rand.Next(0, shipData.Hulls.FindAll(h => h.Class == "Frigate").Count)].Type
                            },
                            shipData, charData, nameGenerations);
                        fCount++;
                    }
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