using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.Database
{
    public class UniverseContext : DbContext
    {
        public DbSet<Universe> Universes { get; set; }
        public DbSet<Star> Stars { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Job> Jobs { get; set; }

        public string DbPath { get; }

        public UniverseContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "universe.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite($"Data Source={DbPath}");
    }

    public class DataAccessLayer
    {
        public List<Universe> GetUniverse()
        {
            UniverseContext uc = new UniverseContext();
            return uc.Universes.Where(u => u.Name == "Pyrus").ToList();
        }
    }
}