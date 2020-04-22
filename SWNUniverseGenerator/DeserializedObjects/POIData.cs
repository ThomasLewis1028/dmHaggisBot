using System;
using System.Collections.Generic;

namespace SWNUniverseGenerator.DeserializedObjects
{
    public class PoiData
    {
        public List<Poi> PointsOfInterest { get; set; }
    }

    public class Poi
    {
        public String Type { get; set; }
        
        public List<String> OccupiedBy { get; set; }
        
        public List<String> Situation { get; set; }
    }
}