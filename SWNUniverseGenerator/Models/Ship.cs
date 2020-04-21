using System;
using System.Collections.Generic;
using SWNUniverseGenerator.DeserializedObjects;

namespace SWNUniverseGenerator.Models
{
    public class Ship : IEntity
    {
        public String ID { get; set; }

        public String Name { get; set; }

        public Hull Hull { get; set; }

        public List<Fitting> Fittings { get; set; }

        public List<Defense> Defenses { get; set; }

        public List<Weapon> Weapons { get; set; }
        
        public String CaptainID { get; set; }
        
        public String PilotID { get; set; }
        
        public String EngineerID { get; set; }
        
        public String CommsID { get; set; }
        
        public String GunnerID { get; set; }
        
        public List<String> CrewID { get; set; }

        public Int32? TotalCost()
        {
            int? fittingsCost = 0;
            int defensesCost = 0;
            int weaponsCost = 0;

            // foreach (var f in Fittings)
            //     fittingsCost += f.Cost ?? 0;
            //
            // foreach (var d in Defenses)
            //     defensesCost += d.Cost;
            //
            // foreach (var w in Weapons)
            //     weaponsCost += w.Cost;

            return Hull.Cost + fittingsCost + defensesCost + weaponsCost;
        }
    }
}