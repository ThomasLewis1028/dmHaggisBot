using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator.DefaultSettings
{
    public class ShipDefaultSettings
    {
        public Int32 Count { get; set; }

        public String ID { get; set; }

        public String Name { get; set; }
        
        public String CaptainID { get; set; }
        
        public String PilotID { get; set; }
        
        public String EngineerID { get; set; }
        
        public String CommsID { get; set; }
        
        public String GunnerID { get; set; }
        
        public List<String> CrewID { get; set; }
        
        public String Type { get; set; }
    }
}