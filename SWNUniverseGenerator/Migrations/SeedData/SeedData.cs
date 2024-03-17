using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.Migrations.SeedData
{
    public class DbInitializer
    {
        public DbInitializer()
        {
        }

        public void Seed(ModelBuilder modelBuilder)
        {
            var hullData = GetShipHullData();
            modelBuilder.Entity<Hull>().HasData(hullData);

            var armamentData = GetShipArmamentData();
            modelBuilder.Entity<Armament>().HasData(armamentData);

            var defenseData = GetShipDefenseData();
            modelBuilder.Entity<Defense>().HasData(defenseData);

            var fittingData = GetShipFittingData();
            modelBuilder.Entity<Fitting>().HasData(fittingData);

            var shipSpecs = DeserializeHulls<ShipSpec>("ShipSpec.json");

            var specData = GetSpecData(hullData, shipSpecs);
            modelBuilder.Entity<Spec>().HasData(specData);

            modelBuilder.Entity<SpecArmament>().HasData(GetSpecArmamentData(specData, armamentData, shipSpecs));
            modelBuilder.Entity<SpecDefense>().HasData(GetSpecDefenseData(specData, defenseData, shipSpecs));
            modelBuilder.Entity<SpecFitting>().HasData(GetSpecFittingData(specData, fittingData, shipSpecs));

            modelBuilder.Entity<Naming>().HasData(GetNamingData());

            var worldTagData = Deserialize<WorldTag>("WorldTag.json");
            var tagData = GetTagData(worldTagData);

            modelBuilder.Entity<Tag>().HasData(tagData);
            modelBuilder.Entity<WorldEnemy>().HasData(GetWorldEnemyData(worldTagData, tagData));
            modelBuilder.Entity<WorldFriend>().HasData(GetWorldFriendData(worldTagData, tagData));
            modelBuilder.Entity<WorldComplication>().HasData(GetWorldComplicationData(worldTagData, tagData));
            modelBuilder.Entity<WorldPlace>().HasData(GetWorldPlaceData(worldTagData, tagData));
            modelBuilder.Entity<WorldThing>().HasData(GetWorldThingData(worldTagData, tagData));

            modelBuilder.Entity<TechLevel>().HasData(GetTechLevelData());
            modelBuilder.Entity<Temperature>().HasData(GetTemperatureData());
            modelBuilder.Entity<Population>().HasData(GetPopulationData());
            modelBuilder.Entity<Atmosphere>().HasData(GetAtmosphereData());
            modelBuilder.Entity<Biosphere>().HasData(GetBiosphereData());

            var problemConflictData = Deserialize<ProblemConflictData>("ProblemConflicts.json");
            var problemRestraintData = Deserialize<ProblemRestraintData>("ProblemRestraint.json");
            var problemTwistData = Deserialize<ProblemTwistData>("ProblemTwist.json");

            modelBuilder.Entity<ProblemConflictSituations>()
                .HasData(GetProblemConflictSituationsData(problemConflictData));
            modelBuilder.Entity<ProblemConflictFocuses>().HasData(GetProblemConflictFocusesData(problemConflictData));
            modelBuilder.Entity<ProblemRestraints>().HasData(GetProblemRestraintsData(problemRestraintData));
            modelBuilder.Entity<ProblemTwists>().HasData(GetProblemTwistsData(problemTwistData));
        }

        public List<WorldEnemy> GetWorldEnemyData(List<WorldTag> worldTagData, List<Tag> tagData)
        {
            var result = new List<WorldEnemy>();

            foreach (var wtd in worldTagData)
            {
                foreach (var ene in wtd.Enemies)
                {
                    result.Add(new WorldEnemy()
                    {
                        TagId = tagData.Find(t => t.Type == wtd.Type).Id,
                        Enemy = ene
                    });
                }
            }

            return result;
        }

        public List<WorldFriend> GetWorldFriendData(List<WorldTag> worldTagData, List<Tag> tagData)
        {
            var result = new List<WorldFriend>();

            foreach (var wtd in worldTagData)
            {
                foreach (var friend in wtd.Friends)
                {
                    result.Add(new WorldFriend()
                    {
                        TagId = tagData.Find(t => t.Type == wtd.Type).Id,
                        Friend = friend
                    });
                }
            }

            return result;
        }

        public List<WorldComplication> GetWorldComplicationData(List<WorldTag> worldTagData, List<Tag> tagData)
        {
            var result = new List<WorldComplication>();

            foreach (var wtd in worldTagData)
            {
                foreach (var complication in wtd.Complications)
                {
                    result.Add(new WorldComplication()
                    {
                        TagId = tagData.Find(t => t.Type == wtd.Type).Id,
                        Complication = complication
                    });
                }
            }

            return result;
        }

        public List<WorldPlace> GetWorldPlaceData(List<WorldTag> worldTagData, List<Tag> tagData)
        {
            var result = new List<WorldPlace>();

            foreach (var wtd in worldTagData)
            {
                foreach (var place in wtd.Places)
                {
                    result.Add(new WorldPlace()
                    {
                        TagId = tagData.Find(t => t.Type == wtd.Type).Id,
                        Place = place
                    });
                }
            }

            return result;
        }

        public List<WorldThing> GetWorldThingData(List<WorldTag> worldTagData, List<Tag> tagData)
        {
            var result = new List<WorldThing>();

            foreach (var wtd in worldTagData)
            {
                foreach (var thing in wtd.Things)
                {
                    result.Add(new WorldThing()
                    {
                        TagId = tagData.Find(t => t.Type == wtd.Type).Id,
                        Thing = thing
                    });
                }
            }

            return result;
        }

        public List<Tag> GetTagData(List<WorldTag> worldTagData)
        {
            var result = new List<Tag>();

            foreach (var wtd in worldTagData)
            {
                result.Add(new Tag()
                {
                    Type = wtd.Type,
                    WorldTagId = wtd.Id,
                    Description = wtd.Description
                });
            }

            return result;
        }

        public List<Biosphere> GetBiosphereData()
        {
            var result = Deserialize<Biosphere>("Biosphere.json");
            return result;
        }

        public List<Atmosphere> GetAtmosphereData()
        {
            var result = Deserialize<Atmosphere>("Atmosphere.json");
            return result;
        }

        public List<Population> GetPopulationData()
        {
            var result = Deserialize<Population>("Population.json");
            return result;
        }

        public List<Temperature> GetTemperatureData()
        {
            var result = Deserialize<Temperature>("Temperature.json");
            return result;
        }

        public List<TechLevel> GetTechLevelData()
        {
            var result = Deserialize<TechLevel>("TechLevel.json");
            return result;
        }

        public List<Hull> GetShipHullData()
        {
            var result = JsonConvert.DeserializeObject<List<Hull>>(ReadManifestData<UniverseContext>("ShipHull.json"),
                new EnumConverter());
            return result;
        }

        public List<Fitting> GetShipFittingData()
        {
            var result =
                JsonConvert.DeserializeObject<List<Fitting>>(ReadManifestData<UniverseContext>("ShipFitting.json"));
            return result;
        }

        public List<Defense> GetShipDefenseData()
        {
            var result =
                JsonConvert.DeserializeObject<List<Defense>>(ReadManifestData<UniverseContext>("ShipDefense.json"));
            return result;
        }

        public List<Armament> GetShipArmamentData()
        {
            var result =
                JsonConvert.DeserializeObject<List<Armament>>(ReadManifestData<UniverseContext>("ShipWeapon.json"));
            return result;
        }

        public List<Naming> GetNamingData()
        {
            List<Naming> result = new List<Naming>();

            var names = JsonConvert.DeserializeObject<List<String>>(
                ReadManifestData<UniverseContext>("StarNames.json"));
            AddNames(names, result, "Star");

            names = JsonConvert.DeserializeObject<List<String>>(ReadManifestData<UniverseContext>("PlanetNames.json"));
            AddNames(names, result, "Planet");

            names = JsonConvert.DeserializeObject<List<String>>(ReadManifestData<UniverseContext>("MaleName.json"));
            AddNames(names, result, "MaleName");

            names = JsonConvert.DeserializeObject<List<String>>(ReadManifestData<UniverseContext>("FemaleName.json"));
            AddNames(names, result, "FemaleName");

            names = JsonConvert.DeserializeObject<List<String>>(ReadManifestData<UniverseContext>("LastName.json"));
            AddNames(names, result, "LastName");

            names = JsonConvert.DeserializeObject<List<String>>(ReadManifestData<UniverseContext>("EyeColor.json"));
            AddNames(names, result, "EyeColor");

            names = JsonConvert.DeserializeObject<List<String>>(ReadManifestData<UniverseContext>("HairColor.json"));
            AddNames(names, result, "HairColor");

            names = JsonConvert.DeserializeObject<List<String>>(ReadManifestData<UniverseContext>("HairStyle.json"));
            AddNames(names, result, "HairStyle");

            names = JsonConvert.DeserializeObject<List<String>>(
                ReadManifestData<UniverseContext>("InitialReactions.json"));
            AddNames(names, result, "InitialReaction");

            names = JsonConvert.DeserializeObject<List<String>>(ReadManifestData<UniverseContext>("Animal.json"));
            AddNames(names, result, "Animal");

            names = JsonConvert.DeserializeObject<List<String>>(ReadManifestData<UniverseContext>("Noun.json"));
            AddNames(names, result, "Noun");

            names = JsonConvert.DeserializeObject<List<String>>(ReadManifestData<UniverseContext>("Adjective.json"));
            AddNames(names, result, "Adjective");

            names = JsonConvert.DeserializeObject<List<String>>(ReadManifestData<UniverseContext>("Preset.json"));
            AddNames(names, result, "Preset");

            return result;
        }

        public List<Spec> GetSpecData(List<Hull> hulls, List<ShipSpec> shipSpecs)
        {
            var result = new List<Spec>();

            AddSpec(shipSpecs, result, hulls);

            return result;
        }

        public List<SpecArmament> GetSpecArmamentData(List<Spec> specData, List<Armament> armamentData,
            List<ShipSpec> shipSpecs)
        {
            var result = new List<SpecArmament>();

            AddSpecArmament(shipSpecs, specData, armamentData, result);

            return result;
        }

        public List<SpecDefense> GetSpecDefenseData(List<Spec> specData, List<Defense> defenseData,
            List<ShipSpec> shipSpecs)
        {
            var result = new List<SpecDefense>();

            AddSpecDefense(shipSpecs, specData, defenseData, result);

            return result;
        }

        public List<SpecFitting> GetSpecFittingData(List<Spec> specData, List<Fitting> fittingData,
            List<ShipSpec> shipSpecs)
        {
            var result = new List<SpecFitting>();

            AddSpecFitting(shipSpecs, specData, fittingData, result);

            return result;
        }

        public List<ProblemConflictSituations> GetProblemConflictSituationsData(List<ProblemConflictData> problemsList)
        {
            var result = new List<ProblemConflictSituations>();

            AddProblemConflictSituations(problemsList, result);

            return result;
        }

        public List<ProblemConflictFocuses> GetProblemConflictFocusesData(List<ProblemConflictData> problemsList)
        {
            var result = new List<ProblemConflictFocuses>();

            AddProblemConflictFocuses(problemsList, result);

            return result;
        }

        public List<ProblemRestraints> GetProblemRestraintsData(List<ProblemRestraintData> restraintsList)
        {
            var result = new List<ProblemRestraints>();

            AddProblemRestraints(restraintsList, result);

            return result;
        }

        public List<ProblemTwists> GetProblemTwistsData(List<ProblemTwistData> twistsList)
        {
            var result = new List<ProblemTwists>();

            AddProblemTwists(twistsList, result);

            return result;
        }

        public void AddNames(List<String> list, List<Naming> namings, String nameType)
        {
            foreach (var s in list)
            {
                if (!String.IsNullOrEmpty(s))
                {
                    var name = new Naming()
                    {
                        NameType = nameType,
                        Name = s
                    };
                    namings.Add(name);
                }
            }
        }

        public void AddSpec(List<ShipSpec> shipSpec, List<Spec> specs, List<Hull> hulls)
        {
            foreach (var spec in shipSpec)
            {
                var newSpec = new Spec
                {
                    SpecName = spec.PresetName,
                    HullId = hulls.Find(h => h.HullType == spec.HullType).Id
                };
                specs.Add(newSpec);
            }
        }

        public void AddSpecArmament(List<ShipSpec> shipSpec, List<Spec> specs, List<Armament> armaments,
            List<SpecArmament> specArmaments)
        {
            specArmaments.AddRange(from spec in shipSpec
                where spec.Weapons != null
                from weapon in spec.Weapons
                select new SpecArmament
                {
                    SpecId = specs.Find(s => s.SpecName == spec.PresetName).Id,
                    ArmamentId = armaments.Find(a => a.Type == weapon).Id
                });
        }

        public void AddSpecDefense(List<ShipSpec> shipSpec, List<Spec> specs, List<Defense> defenses,
            List<SpecDefense> specDefenses)
        {
            specDefenses.AddRange(from spec in shipSpec
                where spec.Defenses != null
                from defense in spec.Defenses
                select new SpecDefense
                {
                    SpecId = specs.Find(s => s.SpecName == spec.PresetName).Id,
                    DefenseId = defenses.Find(a => a.Type == defense).Id
                });
        }

        public void AddSpecFitting(List<ShipSpec> shipSpec, List<Spec> specs, List<Fitting> fittings,
            List<SpecFitting> specFittings)
        {
            specFittings.AddRange(from spec in shipSpec
                where spec.Fittings != null
                from fitting in spec.Fittings
                select new SpecFitting
                {
                    SpecId = specs.Find(s => s.SpecName == spec.PresetName).Id,
                    FittingId = fittings.Find(a => a.Type == fitting).Id
                });
        }

        public void AddProblemConflictSituations(List<ProblemConflictData> conflictList,
            List<ProblemConflictSituations> problemConflictSituationsList)
        {
            problemConflictSituationsList.AddRange(from conflict in conflictList
                where conflict.Situations != null
                from situation in conflict.Situations
                select new ProblemConflictSituations
                {
                    Type = conflict.Type,
                    Situation = situation
                });
        }

        public void AddProblemConflictFocuses(List<ProblemConflictData> conflictList,
            List<ProblemConflictFocuses> problemConflictFocusesList)
        {
            problemConflictFocusesList.AddRange(from conflict in conflictList
                where conflict.Focuses != null
                from focus in conflict.Focuses
                select new ProblemConflictFocuses
                {
                    Type = conflict.Type,
                    Focus = focus
                });
        }

        public void AddProblemRestraints(List<ProblemRestraintData> restraintList,
            List<ProblemRestraints> problemRestraintsList)
        {
            foreach (var restraint in restraintList)
            {
                problemRestraintsList.Add(new ProblemRestraints
                {
                    Restraint = restraint.Restraint
                });
            }
        }

        public void AddProblemTwists(List<ProblemTwistData> twistList,
            List<ProblemTwists> problemTwistsList)
        {
            foreach (var twist in twistList)
            {
                problemTwistsList.Add(new ProblemTwists
                {
                    Twist = twist.Twist
                });
            }
        }

        private string ReadManifestData<TSource>(string embeddedFileName) where TSource : class
        {
            var assembly = typeof(TSource).GetTypeInfo().Assembly;
            var resourceName = assembly.GetManifestResourceNames().First(s =>
                s.EndsWith(embeddedFileName, StringComparison.CurrentCultureIgnoreCase));

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException("Could not load manifest resource stream.");
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public List<TSource> Deserialize<TSource>(String json) where TSource : BaseEntity
        {
            return JsonConvert.DeserializeObject<List<TSource>>(ReadManifestData<UniverseContext>(json));
        }

        public List<TSource> DeserializeHulls<TSource>(String json) where TSource : BaseEntity
        {
            return JsonConvert.DeserializeObject<List<TSource>>(ReadManifestData<UniverseContext>(json),
                new EnumConverter());
        }
    }

    public class EnumConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Hull.HullTypeEnum);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var eTypeVal = typeof(Hull.HullTypeEnum).GetMembers()
                .Where(x => x.GetCustomAttributes(typeof(DescriptionAttribute)).Any())
                .FirstOrDefault(x =>
                    ((DescriptionAttribute)x.GetCustomAttribute(typeof(DescriptionAttribute))).Description ==
                    (string)reader.Value);

            if (eTypeVal == null) return Enum.Parse(typeof(Hull.HullTypeEnum), (string)reader.Value);

            return Enum.Parse(typeof(Hull.HullTypeEnum), eTypeVal.Name);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}