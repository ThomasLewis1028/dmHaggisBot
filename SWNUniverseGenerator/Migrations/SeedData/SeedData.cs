using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            modelBuilder.Entity<ShipHull>().HasData(GetShipHullData());
            modelBuilder.Entity<ShipFitting>().HasData(GetShipFittingData());
            modelBuilder.Entity<ShipDefense>().HasData(GetShipDefenseData());
            modelBuilder.Entity<ShipWeapon>().HasData(GetShipWeaponData());
        }

        public List<ShipHull> GetShipHullData()
        {
            var result = JsonConvert.DeserializeObject<List<ShipHull>>(ReadManifestData<ShipHull>("ShipHull.json"));
            return result;
        }
        
        public List<ShipFitting> GetShipFittingData()
        {
            var result = JsonConvert.DeserializeObject<List<ShipFitting>>(ReadManifestData<ShipFitting>("ShipFitting.json"));
            return result;
        }
        
        public List<ShipDefense> GetShipDefenseData()
        {
            var result = JsonConvert.DeserializeObject<List<ShipDefense>>(ReadManifestData<ShipDefense>("ShipDefense.json"));
            return result;
        }
        
        public List<ShipWeapon> GetShipWeaponData()
        {
            var result = JsonConvert.DeserializeObject<List<ShipWeapon>>(ReadManifestData<ShipWeapon>("ShipWeapon.json"));
            return result;
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