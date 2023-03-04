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
                    List<ShipArmament> shipArmaments = new();
                    List<ShipDefense> shipDefenses = new();
                    List<ShipFitting> shipFittings = new();
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

                        shipRepo.Add(ship);
                        
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

                        // Set the name of the ship based on whatever math I came up with
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

                        // Set the armaments
                        using (var armaRepo = new Repository<ShipArmament>(context))
                        {
                            foreach (var specArmament in context.SpecArmament.Where(s => s.SpecId == ship.SpecId))
                            {
                                var shipArmament = new ShipArmament()
                                {
                                    ArmamentId = specArmament.ArmamentId,
                                    ShipId = ship.Id,
                                    UniverseId = universeId
                                };
                                shipArmaments.Add(shipArmament);
                            }
                            
                            armaRepo.AddRange(shipArmaments);
                        }

                        // Set the defenses
                        using (var defRepo = new Repository<ShipDefense>(context))
                        {
                            foreach (var specDefense in context.SpecDefense.Where(s => s.SpecId == ship.SpecId))
                            {
                                var shipDefense = new ShipDefense()
                                {
                                    DefenseId = specDefense.DefenseId,
                                    ShipId = ship.Id,
                                    UniverseId = universeId
                                };
                                shipDefenses.Add(shipDefense);
                            }
                            
                            defRepo.AddRange(shipDefenses);
                        }

                        // Set the fittings
                        using (var fitRepo = new Repository<ShipFitting>(context))
                        {
                            foreach (var specFitting in context.SpecFitting.Where(s => s.SpecId == ship.SpecId))
                            {
                                var shipFitting = new ShipFitting()
                                {
                                    FittingId = specFitting.FittingId,
                                    ShipId = ship.Id,
                                    UniverseId = universeId
                                };
                                shipFittings.Add(shipFitting);
                            }
                            
                            fitRepo.AddRange(shipFittings);
                        }
                        

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
                                                hullCrewMax + 1 - (shipDefaultSettings.CrewMemberIds?.Count ?? 0)),
                                            ShipId = ship.Id
                                        });
                                }
                            }

                            // Set the crewType on the crewMembers
                            using (var crewRepo = new Repository<CrewMember>(context))
                            {
                                List<CrewMember> crewMembers = new();
                                for (int i = 0; i < crewRepo.Count(c => c.ShipId == ship.Id); i++)
                                {
                                    CrewMember crew;

                                    switch (i)
                                    {
                                        case 0:
                                            crew = context.CrewMember.Skip(i).Take(1).First();
                                            crew.CrewType = CrewMember.CrewEnum.Captain;
                                            break;
                                        case 1:
                                            crew = context.CrewMember.Skip(i).Take(1).First();
                                            crew.CrewType = CrewMember.CrewEnum.Pilot;
                                            break;
                                        case 2:
                                            crew = context.CrewMember.Skip(i).Take(1).First();
                                            crew.CrewType = CrewMember.CrewEnum.Gunner;
                                            break;
                                        case 3:
                                            crew = context.CrewMember.Skip(i).Take(1).First();
                                            crew.CrewType = CrewMember.CrewEnum.Engineer;
                                            break;
                                        case 4:
                                            crew = context.CrewMember.Skip(i).Take(1).First();
                                            crew.CrewType = CrewMember.CrewEnum.Communications;
                                            break;
                                        default:
                                            crew = context.CrewMember.Skip(i).Take(1).First();
                                            crew.CrewType = CrewMember.CrewEnum.Crew;
                                            break;
                                    }

                                    crewMembers.Add(crew);
                                }

                                crewRepo.UpdateRange(crewMembers);
                            }
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
                        
                        shipArmaments.Clear();
                        shipDefenses.Clear();
                        shipFittings.Clear();

                        sCount++;
                    }

                    shipRepo.UpdateRange(ships);

                    ships.Clear();
                    shipArmaments.Clear();
                    shipDefenses.Clear();
                    shipFittings.Clear();
                }
            }
        }
    }
}