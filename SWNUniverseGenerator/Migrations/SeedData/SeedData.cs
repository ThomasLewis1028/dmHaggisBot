using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using SWNUniverseGenerator.DeserializedObjects;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.Migrations
{
    public class SeedData
    {
        public static string ReadManifestData<TSource>(string embeddedFileName) where TSource : class
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

        public static List<ShipHull> GetShipHullData()
        {
            var result = new List<ShipHull>();
            result = JsonConvert.DeserializeObject<List<ShipHull>>(ReadManifestData<ShipHull>("ShipHull.json"));
            return result;
        }
        
        public static List<ShipFitting> GetShipFittingData()
        {
            var result = new List<ShipFitting>();
            result = JsonConvert.DeserializeObject<List<ShipFitting>>(ReadManifestData<ShipFitting>("ShipFitting.json"));
            return result;
        }
        
        public static List<ShipDefense> GetShipDefenseData()
        {
            var result = new List<ShipDefense>();
            result = JsonConvert.DeserializeObject<List<ShipDefense>>(ReadManifestData<ShipDefense>("ShipDefense.json"));
            return result;
        }
        
        public static List<ShipWeapon> GetShipWeaponData()
        {
            var result = new List<ShipWeapon>();
            result = JsonConvert.DeserializeObject<List<ShipWeapon>>(ReadManifestData<ShipWeapon>("ShipWeapon.json"));
            return result;
        }
    }
}