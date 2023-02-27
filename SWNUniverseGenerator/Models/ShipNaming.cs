using System;
using System.Collections.Generic;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.DeserializedObjects
{
    public class ShipNaming : BaseEntity
    {
        public List<String> Adjectives { get; set; }

        public List<String> Animals { get; set; }

        public List<String> Nouns { get; set; }

        public List<String> Presets { get; set; }
    }
}