using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator.DeserializedObjects
{
    public class ShipData
    {
        public List<Hull> Hulls { get; set; }

        public List<Fitting> Fittings { get; set; }

        public List<Defense> Defenses { get; set; }

        public List<Weapon> Weapons { get; set; }

        public List<Presets> Presets { get; set; }

        public Naming Naming { get; set; }
    }

    public class Hull
    {
        public String Type { get; set; }

        public Int32 Cost { get; set; }

        public Int32? Speed { get; set; }

        public Int32 Armor { get; set; }

        public Int32 Hp { get; set; }

        public Int32 CrewMin { get; set; }

        public Int32 CrewMax { get; set; }

        public Int32 Ac { get; set; }

        public Int32 Power { get; set; }

        public Int32 Mass { get; set; }

        public Int32 Hardpoints { get; set; }

        public String Class { get; set; }

        public String Description { get; set; }
    }

    public class Fitting
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

    public class Defense
    {
        public String Type { get; set; }

        public Int32 Cost { get; set; }

        public Boolean CostExtra { get; set; }

        public Int32 Power { get; set; }

        public Int32 Mass { get; set; }

        public Boolean MassExtra { get; set; }

        public String Class { get; set; }

        public String Effect { get; set; }

        public String Description { get; set; }
    }

    public class Weapon
    {
        public String Type { get; set; }

        public Int32 Cost { get; set; }

        public Int32? AmmoCost { get; set; }

        public String Dmg { get; set; }

        public Int32 Power { get; set; }

        public Int32 Mass { get; set; }

        public Int32 Hardpoints { get; set; }

        public String Class { get; set; }

        public Int32 TechLevel { get; set; }

        public String Qualities { get; set; }

        public String Description { get; set; }
    }

    public class Presets
    {
        public String HullType { get; set; }

        public List<Preset> ListPresets { get; set; }
    }

    public class Preset
    {
        public String PresetName { get; set; }

        public Int32 CrewSkill { get; set; }

        public Int32 Cp { get; set; }

        public List<Int32> Weapons { get; set; }

        public List<Int32> Defenses { get; set; }

        public List<Int32> Fittings { get; set; }
    }

    public class Naming
    {
        public List<String> Adjectives { get; set; }

        public List<String> Animals { get; set; }

        public List<String> Nouns { get; set; }

        public List<String> Presets { get; set; }
    }
}