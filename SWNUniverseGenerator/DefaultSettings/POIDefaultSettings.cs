using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator.DefaultSettings
{
    public class PoiDefaultSettings
    {
        public PoiDefaultSettings(
            string universeId = null)
        {
            UniverseId = universeId;
        }

        public Int32[] PoiRange { get; set; }

        public List<String> LocationId { get; set; }

        public String Name { get; set; }

        public string UniverseId { get; set; }
    }
}