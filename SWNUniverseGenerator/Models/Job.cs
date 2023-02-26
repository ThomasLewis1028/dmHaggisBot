using System;
using Microsoft.EntityFrameworkCore;

namespace SWNUniverseGenerator.Models
{
    public class Job
    {
        public String JobId { get; set; }
        public Item Cargo { get; set; }
        public Character Contact { get; set; }
        public Planet Dest { get; set; }
        public int Pay { get; set; }
        /// <summary>
        /// A string value for the universe a given job is tied to
        /// </summary>
        public String UniverseId { get; set; }
    }
}