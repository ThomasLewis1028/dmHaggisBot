using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator.Models
{
    [Obsolete]
    public class ShipNaming : BaseEntity
    {
        [Obsolete] public List<string> Adjectives { get; set; }

        [Obsolete] public List<string> Animals { get; set; }

        [Obsolete] public List<string> Nouns { get; set; }

        [Obsolete] public List<string> Presets { get; set; }
    }
}