using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SWNUniverseGenerator.Models
{
    public class Zone : BaseEntity
    {
        [JsonIgnore]
        [NotMapped]
        public String GetHex => (X < 10 ? "0" + X : X.ToString()) +
                                (Y < 10 ? "0" + Y : Y.ToString());

        public Int32 X { get; set; }

        public Int32 Y { get; set; }
        
        public String UniverseId { get; set; }
    }
}