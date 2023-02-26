using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWNUniverseGenerator.Models
{
    public class Ship : IEntity
    {
        public String Id { get; set; }

        public String Name { get; set; }

        public String Hull { get; set; }

        public List<ShipFitting> Fittings { get; set; }

        public List<ShipDefense> Defenses { get; set; }

        public List<ShipWeapon> Weapons { get; set; }

        public String CaptainId { get; set; }

        public String PilotId { get; set; }

        public String EngineerId { get; set; }

        public String CommsId { get; set; }

        public String GunnerId { get; set; }

        public Int32 CrewSkill { get; set; }

        public Int32 Cp { get; set; }

        public String HomeId { get; set; }
        
        public String LocationId { get; set; }
    }

    public class ShipWeapon
    {
        public ShipWeapon(String name)
        {
            Name = name;
        }
        
        [Key]
        public String Name { get; set; }
    }

    public class ShipFitting
    {
        public ShipFitting(String name)
        {
            Name = name;
        }
        
        [Key]
        public String Name { get; set; }
    }
    
    public class ShipDefense
    {
        public ShipDefense(String name)
        {
            Name = name;
        }
        
        [Key]
        public String Name { get; set; }
    }
}