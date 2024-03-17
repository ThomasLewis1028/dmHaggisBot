using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWNUniverseGenerator.Models
{
    [Table("Ships")]
    public class Ship : BaseEntity
    {
        public String Name { get; set; }
        
        public String HullId { get; set; }

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