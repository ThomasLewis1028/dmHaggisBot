using System;
using System.Collections.Generic;
using SWNUniverseGenerator.DeserializedObjects;

namespace SWNUniverseGenerator.Models
{
    public class Ship : IEntity
    {
        public String Id { get; set; }

        public String Name { get; set; }

        public Hull Hull { get; set; }

        public List<Fitting> Fittings { get; set; }

        public List<Defense> Defenses { get; set; }

        public List<Weapon> Weapons { get; set; }

        public String CaptainId { get; set; }

        public String PilotId { get; set; }

        public String EngineerId { get; set; }

        public String CommsId { get; set; }

        public String GunnerId { get; set; }

        public Int32 CrewSkill { get; set; }

        public Int32 Cp { get; set; }

        public List<String> StoredShips { get; set; }

        public Int32? TotalCost()
        {
            int? fittingsCost = 0;
            int defensesCost = 0;
            int weaponsCost = 0;

            if (Fittings != null)
                foreach (var f in Fittings)
                    fittingsCost += f.Cost ?? 0;

            if (Defenses != null)
                foreach (var d in Defenses)
                    defensesCost += d.Cost;

            if (Weapons != null)
                foreach (var w in Weapons)
                    weaponsCost += w.Cost;

            return Hull.Cost + fittingsCost + defensesCost + weaponsCost;
        }
    }
}