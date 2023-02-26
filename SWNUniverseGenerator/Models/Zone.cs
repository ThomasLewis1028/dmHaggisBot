using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SWNUniverseGenerator.Models
{
    public class Zone : IEntity
    {
        public Zone()
        {
            Id = this.GenerateId();
        }
        
        [JsonIgnore]
        [NotMapped]
        public String GetHex => (X < 10 ? "0" + X : X.ToString()) +
                                (Y < 10 ? "0" + Y : Y.ToString());

        public String Id { get; set; }
        
        [JsonIgnore] public String Name => GetHex;

        public Int32 X { get; set; }

        public Int32 Y { get; set; }

        public String StarId { get; set; }
        
        public List<Planet> Planets { get; set; }

        public List<PointOfInterest> PointsOfInterest { get; set; }
    }
}