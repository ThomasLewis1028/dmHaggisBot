using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWNUniverseGenerator.Models
{
    public class CrewMember : BaseEntity
    {
        public String ShipId { get; set; }
        
        public String CharacterId { get; set; }

        public CrewEnum CrewType { get; set; }
        
        public enum CrewEnum
        {
            Captain,
            Pilot,
            Engineer,
            Communications,
            Gunner,
            Crew,
            Undefined
        }
    }
}