using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.DeserializedObjects;
using SWNUniverseGenerator.Migrations;
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
        public DbSet<ShipWeaponObject> ShipWeaponObject { get; set; }
        public DbSet<ShipWeapon> ShipWeapon { get; set; }
        public DbSet<ShipDefenseObject> ShipDefenseObject { get; set; }
        public DbSet<ShipDefense> ShipDefense { get; set; }
        public DbSet<ShipHullObject> ShipHullObject { get; set; }
        public DbSet<ShipHull> ShipHull { get; set; }
        public DbSet<ShipFittingObject> ShipFittingObject { get; set; }
        public DbSet<ShipFitting> ShipFitting { get; set; }
        public DbSet<Naming> Naming { get; set; }
        public string DbPath { get; }

        public UniverseContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "universe.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // ZONES
            modelBuilder.Entity<Zone>()
                .HasOne<Universe>()
                .WithMany()
                .HasForeignKey(z => z.UniverseId)
                .IsRequired();

            modelBuilder.Entity<Zone>()
                .HasMany<Star>()
                .WithOne();

            modelBuilder.Entity<Zone>()
                .HasMany<PointOfInterest>()
                .WithOne();

            // STARS
            modelBuilder.Entity<Star>()
                .HasOne<Universe>()
                .WithMany()
                .HasForeignKey(s => s.UniverseId)
                .IsRequired();

            // PLANETS
            modelBuilder.Entity<Planet>()
                .HasOne<Zone>()
                .WithMany()
                .HasForeignKey(p => p.ZoneId)
                .IsRequired();

            // SHIPS
            modelBuilder.Entity<Ship>()
                .HasOne<Universe>()
                .WithMany()
                .HasForeignKey(sh => sh.UniverseId)
                .IsRequired();

            // SHIP HULLS
            modelBuilder.Entity<ShipHull>()
                .HasOne<Ship>()
                .WithOne()
                .HasForeignKey<ShipHull>(sh => sh.ShipId);
            
            modelBuilder.Entity<ShipHull>()
                .HasOne<ShipHullObject>()
                .WithOne()
                .HasForeignKey<ShipHull>(sh => sh.ShipHullId);

            // SHIP FITTING
            modelBuilder.Entity<ShipFitting>()
                .HasOne<Ship>()
                .WithOne()
                .HasForeignKey<ShipFitting>(sf => sf.ShipId);

            modelBuilder.Entity<ShipFitting>()
                .HasOne<ShipFittingObject>()
                .WithOne()
                .HasForeignKey<ShipFitting>(sf => sf.ShipFittingId);

            // SHIP WEAPON
            modelBuilder.Entity<ShipWeapon>()
                .HasOne<Ship>()
                .WithOne()
                .HasForeignKey<ShipWeapon>(sf => sf.ShipId);

            modelBuilder.Entity<ShipWeapon>()
                .HasOne<ShipWeaponObject>()
                .WithOne()
                .HasForeignKey<ShipWeapon>(sf => sf.ShipWeaponId);
                        
            // SHIP DEFENSE
            modelBuilder.Entity<ShipDefense>()
                .HasOne<Ship>()
                .WithOne()
                .HasForeignKey<ShipDefense>(sf => sf.ShipId);

            modelBuilder.Entity<ShipDefense>()
                .HasOne<ShipDefenseObject>()
                .WithOne()
                .HasForeignKey<ShipDefense>(sf => sf.ShipDefenseId);

            // CHARACTERS
            modelBuilder.Entity<Character>()
                .HasOne<Universe>()
                .WithMany()
                .HasForeignKey(c => c.UniverseId)
                .IsRequired();

            modelBuilder.Entity<Character>()
                .HasOne<Planet>()
                .WithMany()
                .HasForeignKey(c => c.BirthPlanetId)
                .IsRequired();

            // CREW MEMBER
            modelBuilder.Entity<CrewMember>()
                .HasOne<Character>()
                .WithOne()
                .HasForeignKey<CrewMember>(c => c.CharacterId);

            modelBuilder.Entity<CrewMember>()
                .HasOne<Ship>()
                .WithOne()
                .HasForeignKey<CrewMember>(s => s.ShipId);

            // POINTS OF INTEREST
            modelBuilder.Entity<PointOfInterest>()
                .HasOne<Universe>()
                .WithMany()
                .HasForeignKey(p => p.UniverseId)
                .IsRequired();

            modelBuilder.Entity<Naming>().HasKey(t => new {t.NameType, t.Name});


            new DbInitializer().Seed(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options
                .UseSqlite($"Data Source={DbPath}");
    }
}