using System;
using System.Collections.Generic;
using SWNUniverseGenerator.CreationTools;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.DefaultSettings
{
    public class ShipDefaultSettings
    {
        public ShipDefaultSettings(Int32 count = 10, 
            String homeId = null, 
            String locationId = null,
            String type = null
            )
        {
            Count = count;
            Id = null;
            Name = null;
            CreateCrew = true;
            CaptainId = null;
            PilotId = null;
            EngineerId = null;
            CommunicationsId = null;
            GunnerId = null;
            CrewMemberIds = null;
            Type = type;
            HomeId = homeId;
            LocationId = locationId;
        }
        
        public Int32 Count { get; set; }

        public String Id { get; set; }

        public String Name { get; set; }
        
        public Boolean CreateCrew { get; set; }

        public String CaptainId { get; set; }
        
        public String PilotId { get; set; }
        
        public String EngineerId { get; set; }
        
        public String CommunicationsId { get; set; }
        
        public String GunnerId { get; set; }
        
        public List<String> CrewMemberIds { get; set; }
        
        public String Type { get; set; }
        
        public String HomeId { get; set; }
        
        public String LocationId { get; set; }
    }
}