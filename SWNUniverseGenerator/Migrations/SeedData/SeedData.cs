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
            modelBuilder.Entity<Hull>().HasData(GetShipHullData());
            modelBuilder.Entity<Fitting>().HasData(GetShipFittingData());
            modelBuilder.Entity<Defense>().HasData(GetShipDefenseData());
            modelBuilder.Entity<Armament>().HasData(GetShipWeaponData());
            modelBuilder.Entity<Naming>().HasData(GetNamingData());
        }

        public List<Hull> GetShipHullData()
        {
            var result = JsonConvert.DeserializeObject<List<Hull>>(ReadManifestData<UniverseContext>("ShipHull.json"));
            return result;
        }
        
        public List<Fitting> GetShipFittingData()
        {
            var result = JsonConvert.DeserializeObject<List<Fitting>>(ReadManifestData<UniverseContext>("ShipFitting.json"));
            return result;
        }
        
        public List<Defense> GetShipDefenseData()
        {
            var result = JsonConvert.DeserializeObject<List<Defense>>(ReadManifestData<UniverseContext>("ShipDefense.json"));
            return result;
        }
        
        public List<Armament> GetShipWeaponData()
        {
            var result = JsonConvert.DeserializeObject<List<Armament>>(ReadManifestData<UniverseContext>("ShipWeapon.json"));
            return result;
        }
        
        public List<Naming> GetNamingData()
        {
            List<Naming> result = new List<Naming>();
            
            var names = JsonConvert.DeserializeObject<List<String>>(ReadManifestData<UniverseContext>("StarNames.json"));
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

            names = JsonConvert.DeserializeObject<List<String>>(ReadManifestData<UniverseContext>("InitialReactions.json"));
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

        private void AddNames(List<String> list, List<Naming> namings, String nameType)
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
    }
}