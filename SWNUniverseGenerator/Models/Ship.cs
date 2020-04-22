using System;
using System.Collections.Generic;
using SWNUniverseGenerator.DeserializedObjects;

namespace SWNUniverseGenerator.Models
{
    public class Ship : IEntity
    {
        public String Id { get; set; }

        public String Name { get; set; }

        public String Hull { get; set; }

        public List<String> Fittings { get; set; }

        public List<String> Defenses { get; set; }

        public List<String> Weapons { get; set; }

        public String CaptainId { get; set; }

        public String PilotId { get; set; }

        public String EngineerId { get; set; }

        public String CommsId { get; set; }

        public String GunnerId { get; set; }

        public Int32 CrewSkill { get; set; }

        public Int32 Cp { get; set; }

        public List<String> StoredShips { get; set; }
    }
}