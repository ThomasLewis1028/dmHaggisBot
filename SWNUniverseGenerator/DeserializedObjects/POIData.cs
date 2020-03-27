using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator.DeserializedObjects
{
    public class POIData
    {
        public List<POI> PointsOfInterest { get; set; }
    }

    public class POI
    {
        public String Type { get; set; }
        
        public List<String> OccupiedBy { get; set; }
        
        public List<String> Situation { get; set; }
    }
}