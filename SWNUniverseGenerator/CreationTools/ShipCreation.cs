using System;
using System.Collections.Generic;
using System.Linq;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.CreationTools
{
    internal class ShipCreation
    {
        private static readonly Random Rand = new ();

        public void AddShips(ShipDefaultSettings shipDefaultSettings)
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
                            UniverseId = shipDefaultSettings.UniverseId
                        };

                        Spec spec;

                        using (var specRepo = new Repository<Spec>(context))
                        {
                            using (var hullRepo = new Repository<Hull>(context))
                            {
                                IEntity hull;
                                if (shipDefaultSettings.HullType == Hull.HullTypeEnum.Undefined)
                                {
                                    var hullSwitch = Rand.Next(0, 100);
                                    // Weighted chances for each hull type
                                    hull = hullSwitch switch
                                    {
                                        >= 0 and < 10 => hullRepo
                                            .Search(h => h.HullType == Hull.HullTypeEnum.StrikeFighter)
                                            .First(),
                                        >= 10 and < 25 => hullRepo
                                            .Search(h => h.HullType == Hull.HullTypeEnum.Shuttle)
                                            .First(),
                                        >= 25 and < 50 => hullRepo
                                            .Search(h => h.HullType == Hull.HullTypeEnum.FreeMerchant)
                                            .First(),
                                        >= 50 and < 60 => hullRepo
                                            .Search(h => h.HullType == Hull.HullTypeEnum.PatrolBoat)
                                            .First(),
                                        >= 60 and < 72 => hullRepo
                                            .Search(h => h.HullType == Hull.HullTypeEnum.Corvette)
                                            .First(),
                                        >= 72 and < 81 => hullRepo
                                            .Search(h => h.HullType == Hull.HullTypeEnum.HeavyFrigate)
                                            .First(),
                                        >= 81 and < 86 => hullRepo
                                            .Search(h => h.HullType == Hull.HullTypeEnum.BulkFreighter)
                                            .First(),
                                        >= 86 and < 91 => hullRepo
                                            .Search(h => h.HullType == Hull.HullTypeEnum.FleetCruiser)
                                            .First(),
                                        >= 91 and < 94 => hullRepo
                                            .Search(h => h.HullType == Hull.HullTypeEnum.Battleship)
                                            .First(),
                                        >= 94 and < 95 => hullRepo
                                            .Search(h => h.HullType == Hull.HullTypeEnum.Carrier)
                                            .First(),
                                        >= 95 and < 97 => hullRepo
                                            .Search(h => h.HullType == Hull.HullTypeEnum.FreeMerchant)
                                            .First(), // Free Merchant temporary value
                                        >= 97 and < 100 => hullRepo
                                            .Search(h => h.HullType == Hull.HullTypeEnum.FreeMerchant)
                                            .First(), // Free Merchant temporary value
                                        _ => hullRepo
                                            .Search(h => h.HullType == Hull.HullTypeEnum.FreeMerchant)
                                            .First()
                                    };

                                    ship.HullId = hull.Id;
                                    spec = (Spec)specRepo.Random(s => s.HullId == hull.Id);
                                }
                                else
                                {
                                    ship.HullId = hullRepo.Search(h => h.HullType == shipDefaultSettings.HullType).First().Id;
                                    spec = (Spec)specRepo.Random(s => s.HullId == ship.HullId);
                                }
                            }
                        }

                        shipRepo.Add(ship);

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
                                        name += ((Naming)nameRepo.Random(n => n.NameType == "Adjective")).Name;

                                        name += Rand.Next(0, 10) == 0
                                            ? "-" + ((Naming)nameRepo.Random(n => n.NameType == "Adjective")).Name
                                            : "";

                                        nameType = Rand.Next(0, 2);

                                        if (nameType == 0)
                                            name += " " +
                                                    ((Naming)nameRepo.Random(n => n.NameType == "Animal")).Name;
                                        else
                                            name += " " +
                                                    ((Naming)nameRepo.Random(n => n.NameType == "Noun")).Name;
                                    }
                                    else
                                        name += ((Naming)nameRepo.Random(n => n.NameType == "Noun")).Name;

                                    ship.Name = name;

                                    if (!shipRepo.Any(a => a.Name == name))
                                        break;
                                }
                            }
                        }

                        using (var repo = new Repository<Planet>(context))
                        {
                            ship.HomeId = String.IsNullOrEmpty(shipDefaultSettings.HomeId)
                                ? repo.Random(p => p.UniverseId == shipDefaultSettings.UniverseId).Id
                                : shipDefaultSettings.HomeId;
                            ship.LocationId = String.IsNullOrEmpty(shipDefaultSettings.LocationId)
                                ? repo.Random(p => p.UniverseId == shipDefaultSettings.UniverseId).Id
                                : shipDefaultSettings.LocationId;
                        }

                        // TODO: Fix this to work with new models
                        // ShipPresets shipPresets = shipData.Presets.Find(a => a.HullType == shipHullObject.Type);
                        // ShipSpec shipSpec = shipPresets.ListPresets[Rand.Next(0, shipPresets.ListPresets.Count)];
                        // ship.CrewSkill = shipSpec.CrewSkill;
                        // ship.Cp = shipSpec.Cp;

                        // Set the armaments
                        using (var armamentRepo = new Repository<ShipArmament>(context))
                        {
                            foreach (var specArmament in context.SpecArmament.Where(s => s.SpecId == spec.Id))
                            {
                                var shipArmament = new ShipArmament()
                                {
                                    ArmamentId = specArmament.ArmamentId,
                                    ShipId = ship.Id,
                                    UniverseId = shipDefaultSettings.UniverseId
                                };
                                shipArmaments.Add(shipArmament);
                            }

                            armamentRepo.AddRange(shipArmaments);
                        }

                        // Set the defenses
                        using (var defRepo = new Repository<ShipDefense>(context))
                        {
                            foreach (var specDefense in context.SpecDefense.Where(s => s.SpecId == spec.Id))
                            {
                                var shipDefense = new ShipDefense()
                                {
                                    DefenseId = specDefense.DefenseId,
                                    ShipId = ship.Id,
                                    UniverseId = shipDefaultSettings.UniverseId
                                };
                                shipDefenses.Add(shipDefense);
                            }

                            defRepo.AddRange(shipDefenses);
                        }

                        // Set the fittings
                        using (var fitRepo = new Repository<ShipFitting>(context))
                        {
                            foreach (var specFitting in context.SpecFitting.Where(s => s.SpecId == spec.Id))
                            {
                                var shipFitting = new ShipFitting()
                                {
                                    FittingId = specFitting.FittingId,
                                    ShipId = ship.Id,
                                    UniverseId = shipDefaultSettings.UniverseId
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
                                    var hullCrewMin = ((Hull)hullRepo.Search(h =>
                                        h.Id == ship.HullId).First()).CrewMin;

                                    var hullCrewMax = ((Hull)hullRepo.Search(h =>
                                        h.Id == ship.HullId).First()).CrewMax;

                                    new CharCreation().AddCharacters(
                                        new CharacterDefaultSettings
                                        {
                                            UniverseId = shipDefaultSettings.UniverseId,
                                            Count = Rand.Next(hullCrewMin,
                                                hullCrewMax + 1 - (shipDefaultSettings.CrewMemberIds?.Count ?? 0)),
                                            ShipId = ship.Id
                                        });
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

                        // TODO: Fix this to work with new models
                        // If the ship has the Ship Bay/Fighter or Ship Bay/Frigate fitting, generate a ship to go in that space
                        using (var fittingRepo = new Repository<Fitting>(context))
                        {
                            var fighterBayCount = shipFittings.Count(sf =>
                                sf.FittingId == fittingRepo.Search(f => f.Type == "Ship bay/fighter").First().Id);
                            fighterBayCount = Rand.Next(0, fighterBayCount + 1);

                            var frigateBayCount = shipFittings.Count(sf =>
                                sf.FittingId == fittingRepo.Search(f => f.Type == "Ship bay/frigate").First().Id);
                            frigateBayCount = Rand.Next(0, frigateBayCount + 1);

                            if (fighterBayCount > 0)
                            {
                                new ShipCreation().AddShips(new ShipDefaultSettings
                                {
                                    UniverseId = shipDefaultSettings.UniverseId,
                                    Count = fighterBayCount,
                                    HullClass = Hull.HullClassEnum.Fighter,
                                    HomeId = ship.Id,
                                    LocationId = ship.Id,
                                    CreateCrew = false
                                });
                            }

                            if (frigateBayCount > 0)
                            {
                                new ShipCreation().AddShips(new ShipDefaultSettings
                                {
                                    UniverseId = shipDefaultSettings.UniverseId,
                                    Count = frigateBayCount,
                                    HullClass = Hull.HullClassEnum.Frigate,
                                    HomeId = ship.Id,
                                    LocationId = ship.Id,
                                    CreateCrew = false
                                });
                            }
                        }

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