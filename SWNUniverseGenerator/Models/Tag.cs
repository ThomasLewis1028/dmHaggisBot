using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWNUniverseGenerator.Models
{
    public class Tag : BaseEntity
    {
        public String Type { get; set; }
        
        [NotMapped]
        public String WorldTagId { get; set; }
        
        public String Description { get; set; }
    }
}