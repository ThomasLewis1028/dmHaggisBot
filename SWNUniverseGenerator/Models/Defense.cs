using System;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.Models
{
    public class Defense : BaseEntity
    {
        public Int32 Cost { get; set; }

        public Boolean CostExtra { get; set; }

        public Int32 Power { get; set; }

        public Int32 Mass { get; set; }

        public Boolean MassExtra { get; set; }

        public String Class { get; set; }

        public String Effect { get; set; }

        public String Description { get; set; }
    }
}