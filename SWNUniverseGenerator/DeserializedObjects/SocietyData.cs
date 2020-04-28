using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator.DeserializedObjects
{
    public class SocietyData
    {
        public Societies Societies { get; set; }

        public Rulers Rulers { get; set; }

        public Ruled Ruled { get; set; }

        public Flavors Flavors { get; set; }
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

    public class Ruled
    {
        public List<String> Contentments { get; set; }

        public List<String> LastMajorThreats { get; set; }

        public List<String> Powers { get; set; }

        public List<String> Uniformities { get; set; }

        public List<String> MainConflicts { get; set; }

        public List<String> Trends { get; set; }
    }

    public class Flavors
    {
        public List<String> BasicFlavors { get; set; }
        
        public List<String> OutsiderTreatments { get; set; }

        public List<String> PrimaryVirtues { get; set; }

        public List<String> PrimaryVices { get; set; }

        public List<String> XenophiliaDegrees { get; set; }

        public List<String> PossiblePatrons { get; set; }

        public List<String> Customs { get; set; }
    }
}