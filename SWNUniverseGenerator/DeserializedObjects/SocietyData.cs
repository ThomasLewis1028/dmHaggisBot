using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator.DeserializedObjects
{
    public class SocietyData
    {
        public Societies Societies { get; set; }

        public Rulers Rulers { get; set; }
    }

    public class Societies
    {
        public List<String> PriorCultures { get; set; }

        public List<String> OtherSocieties { get; set; }

        public List<String> MainRemnants { get; set; }

        public List<String> SocietyAges { get; set; }

        public List<String> ImportantResources { get; set; }

        public List<String> FoundingReasons { get; set; }
    }

    public class Rulers
    {
        public List<String> GeneralSecurities { get; set; }

        public List<String> LegitimacySources { get; set; }

        public List<String> MainRulerConflicts { get; set; }

        public List<String> RuleCompletions { get; set; }

        public List<String> RuleForms { get; set; }

        public List<String> MainPopConflicts { get; set; }
    }
}