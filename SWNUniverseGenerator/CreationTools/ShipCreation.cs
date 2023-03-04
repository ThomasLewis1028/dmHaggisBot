using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.DeserializedObjects;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.CreationTools
{
    internal class ShipCreation
    {
        private static readonly Random Rand = new Random();

        public void AddShips(String universeId, ShipDefaultSettings shipDefaultSettings)
        {
            using (var context = new UniverseContext())
            {
                using (var shipRepo = new Repository<Ship>(context))
                {
                    List<Ship> ships = new();
                    // Set the defaults
                    var count = shipDefaultSettings.Count;

                    var sCount = 0;
                    while (sCount < count)
                    {
                        var ship = new Ship()
                        {
                            UniverseId = universeId
                        };

                        using (var specRepo = new Repository<Spec>(context))
                            ship.SpecId = specRepo.Random().Id;

                        // Set the type of ship
                        // if (string.IsNullOrEmpty(shipDefaultSettings.Type))
                        // {
                        //     // var hullSwitch = Rand.Next(0, 100);
                        //     //
                        //     // // Weighted chances for each hull type
                        //     // shipHullObject = hullSwitch switch
                        //     // {
                        //     //     { } n when (n >= 0 && n < 10) => shipData.Hulls[0],
                        //     //     { } n when (n >= 10 && n < 25) => shipData.Hulls[1],
                        //     //     { } n when (n >= 25 && n < 50) => shipData.Hulls[2],
                        //     //     { } n when (n >= 50 && n < 60) => shipData.Hulls[3],
                        //     //     { } n when (n >= 60 && n < 72) => shipData.Hulls[4],
                        //     //     { } n when (n >= 72 && n < 81) => shipData.Hulls[5],
                        //     //     { } n when (n >= 81 && n < 86) => shipData.Hulls[6],
                        //     //     { } n when (n >= 86 && n < 91) => shipData.Hulls[7],
                        //     //     { } n when (n >= 91 && n < 94) => shipData.Hulls[8],
                        //     //     { } n when (n >= 94 && n < 95) => shipData.Hulls[9],
                        //     //     { } n when (n >= 95 && n < 97) => shipData.Hulls[2], // Free Merchant temporary value
                        //     //     { } n when (n >= 97 && n < 100) => shipData.Hulls[2], // Free Merchant temporary value
                        //     //     _ => shipData.Hulls[2]
                        //     // };
                        //     
                        // }
                        // else if (shipData.Hulls.Exists(h => h.Type == type))
                        //     shipHullObject = shipData.Hulls.Find(h => h.Type == type);
                        // else
                        //     throw new FileLoadException("No ship of this type exists");

                        if (string.IsNullOrEmpty(shipDefaultSettings.Name))
                        {
                            while (true)
                            {
                                var name = "";
                                var nameType = Rand.Next(0, 100);

                                using (var nameRepo = new Repository<Naming>(context))
                                {
                                    if (nameType > 0)
                                    {
                                        name += ((Naming) nameRepo.Random(n => n.NameType == "Adjective")).Name;

                                        name += Rand.Next(0, 10) == 0
                                            ? "-" + ((Naming) nameRepo.Random(n => n.NameType == "Adjective")).Name
                                            : "";

                                        nameType = Rand.Next(0, 2);

                                        if (nameType == 0)
                                            name += " " +
                                                    ((Naming) nameRepo.Random(n => n.NameType == "Animal")).Name;
                                        else
                                            name += " " +
                                                    ((Naming) nameRepo.Random(n => n.NameType == "Noun")).Name;
                                    }
                                    else
                                        name += ((Naming) nameRepo.Random(n => n.NameType == "Noun")).Name;

                                    ship.Name = name;

                                    if (!shipRepo.Any(a => a.Name == name))
                                        break;
                                }
                            }
                        }

                        // ship.Hull = shipHull;
                        using (var repo = new Repository<Planet>(context))
                        {
                            ship.HomeId = String.IsNullOrEmpty(shipDefaultSettings.HomeId)
                                ? repo.Random(p => p.UniverseId == universeId).Id
                                : shipDefaultSettings.HomeId;
                            ship.LocationId = String.IsNullOrEmpty(shipDefaultSettings.LocationId)
                                ? repo.Random(p => p.UniverseId == universeId).Id
                                : shipDefaultSettings.LocationId;
                        }

                        // ShipPresets shipPresets = shipData.Presets.Find(a => a.HullType == shipHullObject.Type);
                        // ShipSpec shipSpec = shipPresets.ListPresets[Rand.Next(0, shipPresets.ListPresets.Count)];
                        // ship.CrewSkill = shipSpec.CrewSkill;
                        // ship.Cp = shipSpec.Cp;

                        // // Set the weapons
                        // if (shipSpec.Weapons != null)
                        // {
                        //     // ship.Weapons = new List<ShipWeapon>();
                        //     // foreach (var w in shipPreset.Weapons)
                        //         // ship.Weapons.Add(new shipData.Weapons[w]);
                        // }
                        //
                        // // Set the defenses
                        // if (shipSpec.Defenses != null)
                        // {
                        //     // ship.Defenses = new List<ShipDefense>();
                        //     // foreach (var d in shipPreset.Defenses)
                        //         // ship.Defenses.Add(new ShipDefense(shipData.Defenses[d]));
                        // }
                        //
                        // // Set the fittings
                        // if (shipSpec.Fittings != null)
                        // {
                        //     // ship.Fittings = new List<ShipFitting>();
                        //     // foreach (var f in shipPreset.Fittings)
                        //         // ship.Fittings.Add(new ShipFitting(shipData.Fittings[f]));
                        // }
                        //
                        // // Set the crew if the CrewId is not null
                        // if (shipDefaultSettings.CrewMembers != null)
                        //     foreach (var c in shipDefaultSettings.CrewMembers)
                        //         universe.Characters.Find(a => a == c).ShipId = ship.Id;


                        // Create the crew for the ship
                        if (shipDefaultSettings.CreateCrew)
                        {
                            using (var hullRepo = new Repository<Hull>(context))
                            {
                                using (var specRepo = new Repository<Spec>(context))
                                {
                                    var hullId = ((Spec) specRepo.Search(s => s.Id == ship.SpecId).First()).HullId;

                                    var hullCrewMin = ((Hull) hullRepo.Search(h =>
                                        h.Id == hullId).First()).CrewMin;

                                    var hullCrewMax = ((Hull) hullRepo.Search(h =>
                                        h.Id == hullId).First()).CrewMax;

                                    new CharCreation().AddCharacters(universeId,
                                        new CharacterDefaultSettings
                                        {
                                            Count = Rand.Next(hullCrewMin,
                                                hullCrewMax + 1 - (shipDefaultSettings.CrewMembers?.Count ?? 0)),
                                            ShipId = ship.Id
                                        });
                                }
                            }

                            //     using (var crewRepo = new Repository<CrewMember>(context))
                            //     {
                            //         var crewMembers = crewRepo.Search(c => c.ShipId == ship.Id);
                            //
                            //         
                            //     }
                            //
                            //
                            // var crewList = (from c in universe.Characters where c.ShipId == ship.Id select c).ToList();
                            //
                            //     ship.Captain = shipDefaultSettings.Captain != null
                            //         ? crewList[0] != null ? null : crewList[0]
                            //         : shipDefaultSettings.Captain;
                            //     ship.Pilot = shipDefaultSettings.Pilot != null
                            //         ? crewList.Count < 2
                            //             ? null
                            //             : crewList[1]
                            //         : shipDefaultSettings.Pilot;
                            //     ship.Gunner = shipDefaultSettings.Gunner != null
                            //         ? crewList.Count < 3
                            //             ? null
                            //             : crewList[2]
                            //         : shipDefaultSettings.Gunner;
                            //     ship.Engineer = shipDefaultSettings.Engineer != null
                            //         ? crewList.Count < 4
                            //             ? null
                            //             : crewList[3]
                            //         : shipDefaultSettings.Engineer;
                            //     ship.Comms = shipDefaultSettings.Comms != null
                            //         ? crewList.Count < 5
                            //             ? null
                            //             : crewList[4]
                            //         : shipDefaultSettings.Comms;
                            //
                            //
                            // // Tie the main crew to the ship
                            // if (ship.Captain != null)
                            //     universe.Characters.Find(c => c == ship.Captain).Title = "Captain";
                            // if (ship.Pilot != null)
                            //     universe.Characters.Find(c => c == ship.Pilot).Title = "Pilot";
                            // if (ship.Gunner != null)
                            //     universe.Characters.Find(c => c == ship.Gunner).Title = "Gunner";
                            // if (ship.Engineer != null)
                            //     universe.Characters.Find(c => c == ship.Engineer).Title = "Engineer";
                            // if (ship.Comms != null)
                            //     universe.Characters.Find(c => c == ship.Comms).Title = "Comms Expert";
                        }

                        // If the ship has the Ship Bay/Fighter fitting, generate a ship to go in that space
                        // if (ship.Fittings.Any(f => f.Name == "Ship bay/fighter"))
                        // {
                        //     var fighterHangarCount = ship.Fittings.FindAll(f => f.Name == "Ship bay/fighter").Count;
                        //
                        //     ShipCreation shipCreation = new ShipCreation();
                        //
                        //     var randFhc = Rand.Next(0, fighterHangarCount);
                        //     var fCount = 0;
                        //     while (fCount < randFhc)
                        //     {
                        //         universeId = shipCreation.AddShips(universeId,
                        //             new ShipDefaultSettings
                        //             {
                        //                 Count = 1,
                        //                 CreateCrew = false,
                        //                 HomeId = ship.Id,
                        //                 Type = shipData.Hulls.FindAll(h => h.Class == "Fighter")[
                        //                     Rand.Next(0, shipData.Hulls.FindAll(h => h.Class == "Fighter").Count)].Type
                        //             },
                        //             shipData, charData, nameGenerations);
                        //         fCount++;
                        //     }
                        // }

                        // If the ship has the Ship Bay/Frigate fitting, generate a ship to go in that space
                        // if (ship.Fittings.Any(f => f.Name == "Ship bay/frigate"))
                        // {
                        //     var frigateHangarCount = ship.Fittings.FindAll(f => f.Name == "Ship bay/frigate").Count;
                        //
                        //     ShipCreation shipCreation = new ShipCreation();
                        //
                        //     var randFhc = Rand.Next(0, frigateHangarCount);
                        //     var fCount = 0;
                        //     while (fCount < randFhc)
                        //     {
                        //         universeId = shipCreation.AddShips(universeId,
                        //             new ShipDefaultSettings
                        //             {
                        //                 Count = 1,
                        //                 CreateCrew = false,
                        //                 HomeId = ship.Id,
                        //                 Type = shipData.Hulls.FindAll(h => h.Class == "Frigate")[
                        //                     Rand.Next(0, shipData.Hulls.FindAll(h => h.Class == "Frigate").Count)].Type
                        //             },
                        //             shipData, charData, nameGenerations);
                        //         fCount++;
                        //     }
                        // }

                        ships.Add(ship);

                        sCount++;
                    }

                    shipRepo.AddRange(ships);
                    ships.Clear();
                }
            }
        }
    }
}