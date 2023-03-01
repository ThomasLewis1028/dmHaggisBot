using System;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<Hull> Hull { get; set; }
        public DbSet<Armament> Armament { get; set; }
        public DbSet<Defense> Defense { get; set; }
        public DbSet<Fitting> Fitting { get; set; }
        public DbSet<ShipArmament> ShipArmament { get; set; }
        public DbSet<ShipDefense> ShipDefense { get; set; }
        public DbSet<ShipFitting> ShipFitting { get; set; }
        public DbSet<Spec> Spec { get; set; }
        public DbSet<SpecArmament> SpecArmament { get; set; }
        public DbSet<SpecDefense> SpecDefense { get; set; }
        public DbSet<SpecFitting> SpecFitting { get; set; }
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

            modelBuilder.Entity<Ship>()
                .HasOne<Spec>()
                .WithMany()
                .HasForeignKey(sh => sh.SpecId)
                .IsRequired();

            // SHIP ARMAMENT
            modelBuilder.Entity<ShipArmament>()
                .HasOne<Ship>()
                .WithOne()
                .HasForeignKey<ShipArmament>(sf => sf.ShipId);

            modelBuilder.Entity<ShipArmament>()
                .HasOne<Armament>()
                .WithOne()
                .HasForeignKey<ShipArmament>(sf => sf.ArmamentId);
            
            // SHIP DEFENSE
            modelBuilder.Entity<ShipDefense>()
                .HasOne<Ship>()
                .WithOne()
                .HasForeignKey<ShipDefense>(sf => sf.ShipId);
            
            modelBuilder.Entity<ShipDefense>()
                .HasOne<Defense>()
                .WithOne()
                .HasForeignKey<ShipDefense>(sf => sf.DefenseId);
            
            // SHIP FITTING
            modelBuilder.Entity<ShipFitting>()
                .HasOne<Ship>()
                .WithOne()
                .HasForeignKey<ShipFitting>(sf => sf.ShipId);

            modelBuilder.Entity<ShipFitting>()
                .HasOne<Fitting>()
                .WithOne()
                .HasForeignKey<ShipFitting>(sf => sf.FittingId);
            
            // SPEC
            modelBuilder.Entity<Spec>()
                .HasOne<Hull>()
                .WithOne()
                .HasForeignKey<Spec>(s => s.HullId);

            // SPEC ARMAMENT
            modelBuilder.Entity<SpecArmament>()
                .HasOne<Spec>()
                .WithOne()
                .HasForeignKey<SpecArmament>(sf => sf.SpecId);

            modelBuilder.Entity<SpecArmament>()
                .HasOne<Armament>()
                .WithOne()
                .HasForeignKey<SpecArmament>(sf => sf.ArmamentId);
                        
            // SPEC DEFENSE
            modelBuilder.Entity<SpecDefense>()
                .HasOne<Spec>()
                .WithOne()
                .HasForeignKey<SpecDefense>(sf => sf.SpecId);

            modelBuilder.Entity<SpecDefense>()
                .HasOne<Defense>()
                .WithOne()
                .HasForeignKey<SpecDefense>(sf => sf.DefenseId);
            
            // SPEC FITTING
            modelBuilder.Entity<SpecFitting>()
                .HasOne<Spec>()
                .WithOne()
                .HasForeignKey<SpecFitting>(sf => sf.SpecId);

            modelBuilder.Entity<SpecFitting>()
                .HasOne<Fitting>()
                .WithOne()
                .HasForeignKey<SpecFitting>(sf => sf.FittingId);

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