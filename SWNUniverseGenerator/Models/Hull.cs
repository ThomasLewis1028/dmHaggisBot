using System;
using System.ComponentModel;

namespace SWNUniverseGenerator.Models
{
    public class Hull : BaseEntity
    {
        public HullTypeEnum HullType { get; set; }

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

        public HullClassEnum Class { get; set; }

        public String Description { get; set; }
        
        public enum HullTypeEnum
        {
            [Description("Battleship")]
            Battleship,
            [Description("Carrier")]
            Carrier,
            [Description("Strike Fighter")]
            StrikeFighter,
            [Description("Patrol Boat")]
            PatrolBoat,
            [Description("Small Station")]
            SmallStation,
            [Description("Bulk Freighter")]
            BulkFreighter,
            [Description("Fleet Cruiser")]
            FleetCruiser,
            [Description("Corvette")]
            Corvette,
            [Description("Free Merchant")]
            FreeMerchant,
            [Description("Heavy Frigate")]
            HeavyFrigate,
            [Description("Large Station")]
            LargeStation,
            [Description("Shuttle")]
            Shuttle,
            [Description("Undefined")]
            Undefined
        }

        public enum HullClassEnum
        {
            [Description("Capital")]
            Capital,
            [Description("Fighter")]
            Fighter,
            [Description("Frigate")]
            Frigate,
            [Description("Cruiser")]
            Cruiser,
            [Description("Undefined")]
            Undefined

        }
    }
    
}