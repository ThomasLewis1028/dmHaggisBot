using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator.DeserializedObjects
{
    public class BeastData
    {
        public List<BeastType> BeastTypes { get; set; }

        public List<String> BasicFeatures { get; set; }

        public List<String> BodyPlans { get; set; }

        public List<String> LimbNovelties { get; set; }

        public List<String> SkinNovelties { get; set; }

        public List<String> MainWeapons { get; set; }

        public List<String> Sizes { get; set; }

        public Traits Traits { get; set; }

        public List<String> HarmfulDischarges { get; set; }

        public Poisons Poisons { get; set; }
    }

    public class BeastType
    {
        public String Type { get; set; }

        public Int32 HD { get; set; }

        public Int32 AC { get; set; }

        public Int32 Attack { get; set; }

        public String Damage { get; set; }

        public Int32 AttackMult { get; set; }

        public Int32 Move { get; set; }

        public Int32 Morale { get; set; }

        public Int32 Skills { get; set; }

        public Int32 Class { get; set; }
    }

    public class Traits
    {
        public List<String> Predators { get; set; }

        public List<String> Prey { get; set; }

        public List<String> Scavengers { get; set; }
    }

    public class Poisons
    {
        public List<String> Types { get; set; }

        public List<String> Onsets { get; set; }

        public List<String> Durations { get; set; }
    }
}