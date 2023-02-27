using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SWNUniverseGenerator.DeserializedObjects;

namespace SWNUniverseGenerator.Models
{
    [Table("Ships")]
    public class Ship : BaseEntity
    {
        public String Name { get; set; }

        public ShipHull Hull { get; set; }

        public List<ShipFitting> Fittings { get; set; }

        public List<ShipDefense> Defenses { get; set; }

        public List<ShipWeapon> Weapons { get; set; }

        public Int32 CrewSkill { get; set; }

        public Int32 Cp { get; set; }

        public String HomeId { get; set; }
        
        public String LocationId { get; set; }
        
        /// <summary>
        /// A string value for the universe a given ship is tied to
        /// </summary>
        public String UniverseId { get; set; }
    }
}