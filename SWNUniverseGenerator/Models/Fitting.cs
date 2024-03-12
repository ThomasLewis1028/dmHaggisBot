using System;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.Models
{
    public class Fitting : BaseEntity
    {
        
        public String Type { get; set; }

        public Int32? Cost { get; set; }

        public Boolean CostExtra { get; set; }

        public Int32 Power { get; set; }

        public Boolean PowerExtra { get; set; }

        public Double Mass { get; set; }

        public Boolean MassExtra { get; set; }

        public String Class { get; set; }

        public String Effect { get; set; }

        public String Description { get; set; }
    }
}