using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.Migrations
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

            var shipSpecs = Deserialize<ShipSpec>("ShipSpec.json");

            var specData = GetSpecData(hullData, shipSpecs);
            modelBuilder.Entity<Spec>().HasData(specData);

            modelBuilder.Entity<SpecArmament>().HasData(GetSpecArmamentData(specData, armamentData, shipSpecs));
            modelBuilder.Entity<SpecDefense>().HasData(GetSpecDefenseData(specData, defenseData, shipSpecs));
            modelBuilder.Entity<SpecFitting>().HasData(GetSpecFittingData(specData, fittingData, shipSpecs));

            modelBuilder.Entity<Naming>().HasData(GetNamingData());
        }

        public List<Hull> GetShipHullData()
        {
            var result = JsonConvert.DeserializeObject<List<Hull>>(ReadManifestData<UniverseContext>("ShipHull.json"));
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
                var newSpec = new Spec()
                {
                    SpecName = spec.PresetName,
                    HullId = hulls.Find(h => h.Type == spec.HullType).Id
                };
                specs.Add(newSpec);
            }
        }

        public void AddSpecArmament(List<ShipSpec> shipSpec, List<Spec> specs, List<Armament> armaments,
            List<SpecArmament> specArmaments)
        {
            foreach (var spec in shipSpec)
            {
                if(spec.Weapons != null)
                {
                    foreach (var weapon in spec.Weapons)
                    {
                        var newSpec = new SpecArmament()
                        {
                            SpecId = specs.Find(s => s.SpecName == spec.PresetName).Id,
                            ArmamentId = armaments.Find(a => a.Type == weapon).Id
                        };

                        specArmaments.Add(newSpec);
                    }
                }
            }
        }

        public void AddSpecDefense(List<ShipSpec> shipSpec, List<Spec> specs, List<Defense> defenses,
            List<SpecDefense> specDefenses)
        {
            foreach (var spec in shipSpec)
            {
                if(spec.Defenses != null)
                {
                    foreach (var defense in spec.Defenses)
                    {
                        var newSpec = new SpecDefense()
                        {
                            SpecId = specs.Find(s => s.SpecName == spec.PresetName).Id,
                            DefenseId = defenses.Find(a => a.Type == defense).Id
                        };

                        specDefenses.Add(newSpec);
                    }
                }
            }
        }

        public void AddSpecFitting(List<ShipSpec> shipSpec, List<Spec> specs, List<Fitting> fittings,
            List<SpecFitting> specFittings)
        {
            foreach (var spec in shipSpec)
            {
                if(spec.Fittings != null)
                {
                    foreach (var fitting in spec.Fittings)
                    {
                        var newSpec = new SpecFitting()
                        {
                            SpecId = specs.Find(s => s.SpecName == spec.PresetName).Id,
                            FittingId = fittings.Find(a => a.Type == fitting).Id
                        };

                        specFittings.Add(newSpec);
                    }
                }
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
    }
}