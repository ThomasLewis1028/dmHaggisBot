using System;
using System.Collections.Generic;
using SWNUniverseGenerator.CreationTools;

namespace SWNUniverseGenerator.DefaultSettings
{
    public class ShipDefaultSettings
    {
        public ShipDefaultSettings()
        {
            Count = -1;
            Id = null;
            Name = null;
            CreateCrew = true;
            CaptainId = null;
            PilotId = null;
            EngineerId = null;
            CommsId = null;
            GunnerId = null;
            CrewId = null;
            Type = null;
            HomeId = null;
            LocationId = null;
        }
        
        public Int32 Count { get; set; }

        public String Id { get; set; }

        public String Name { get; set; }
        
        public Boolean CreateCrew { get; set; }

        public String CaptainId { get; set; }
        
        public String PilotId { get; set; }
        
        public String EngineerId { get; set; }
        
        public String CommsId { get; set; }
        
        public String GunnerId { get; set; }
        
        public List<String> CrewId { get; set; }
        
        public String Type { get; set; }
        
        public String HomeId { get; set; }
        
        public String LocationId { get; set; }
    }
}