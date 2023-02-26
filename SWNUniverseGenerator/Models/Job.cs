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
    }
}