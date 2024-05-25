using System;
using LibGit2Sharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SWNUniverseGenerator.Migrations;
using SWNUniverseGenerator.Migrations.SeedData;
using SWNUniverseGenerator.Models;
using Tag = SWNUniverseGenerator.Models.Tag;

namespace SWNUniverseGenerator.Database
{
    public class UniverseContext : DbContext
    {
        public DbSet<Armament> Armament { get; set; }
        public DbSet<Atmosphere> Atmosphere { get; set; }
        public DbSet<Biosphere> Biosphere { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<CrewMember> CrewMember { get; set; }
        public DbSet<Defense> Defense { get; set; }
        public DbSet<Fitting> Fitting { get; set; }
        public DbSet<Hull> Hull { get; set; }
        public DbSet<Naming> Naming { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }
        public DbSet<PoiType> PoiTypes { get; set; }
        public DbSet<PoiOccupiedBy> PoiOccupiedBys { get; set; }
        public DbSet<PoiSituation> PoiSituations { get; set; }
        public DbSet<Population> Population { get; set; }
        public DbSet<LocationProblem> Problems { get; set; }
        public DbSet<ProblemConflictFocuses> ProblemConflictFocuses { get; set; }
        public DbSet<ProblemConflictSituations> ProblemConflictSituations { get; set; }
        public DbSet<ProblemRestraints> ProblemRestraints { get; set; }
        public DbSet<ProblemTwists> ProblemTwists { get; set; }
        public DbSet<ShipArmament> ShipArmament { get; set; }
        public DbSet<ShipDefense> ShipDefense { get; set; }
        public DbSet<ShipFitting> ShipFitting { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Spec> Spec { get; set; }
        public DbSet<SpecArmament> SpecArmament { get; set; }
        public DbSet<SpecDefense> SpecDefense { get; set; }
        public DbSet<SpecFitting> SpecFitting { get; set; }
        public DbSet<Star> Stars { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<TechLevel> TechLevel { get; set; }
        public DbSet<Temperature> Temperature { get; set; }
        public DbSet<Universe> Universes { get; set; }
        public DbSet<WorldComplication> WorldComplication { get; set; }
        public DbSet<WorldEnemy> WorldEnemy { get; set; }
        public DbSet<WorldFriend> WorldFriend { get; set; }
        public DbSet<WorldPlace> WorldPlace { get; set; }
        public DbSet<WorldThing> WorldThing { get; set; }
        public DbSet<Zone> Zones { get; set; }

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
            modelBuilder.Entity<Zone>().HasKey(e => new { e.Id });

            modelBuilder.Entity<Zone>()
                .HasOne<Universe>()
                .WithMany()
                .HasForeignKey(z => z.UniverseId)
                .IsRequired();

            modelBuilder.Entity<Zone>()
                .HasMany<Star>()
                .WithOne();

            // modelBuilder.Entity<Zone>()
            //     .HasMany<PointOfInterest>()
            //     .WithOne();

            // STARS
            modelBuilder.Entity<Star>().HasKey(e => new { e.Id });

            modelBuilder.Entity<Star>()
                .HasOne<Universe>()
                .WithMany()
                .HasForeignKey(s => s.UniverseId)
                .IsRequired();

            // PLANETS
            modelBuilder.Entity<Planet>().HasKey(e => new { e.Id });

            modelBuilder.Entity<Planet>()
                .HasOne<Zone>()
                .WithMany()
                .HasForeignKey(p => p.ZoneId)
                .IsRequired();
            
            // CITIES
            modelBuilder.Entity<City>().HasKey(e => new { e.Id });

            modelBuilder.Entity<City>()
                .HasOne<Planet>()
                .WithMany()
                .HasForeignKey(c => c.PlanetId)
                .IsRequired();

            ShipModelCreating(modelBuilder);
            CharacterModelCreating(modelBuilder);
            TagModelCreating(modelBuilder);
            ProblemModelCreating(modelBuilder);
            PoiModelCreating(modelBuilder);

            // Naming
            modelBuilder.Entity<Naming>().HasKey(t => new { t.NameType, t.Name });

            new DbInitializer().Seed(modelBuilder);
        }

        private void TagModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().HasKey(e => new { e.Id });
            modelBuilder.Entity<WorldEnemy>().HasKey(e => new { e.Id });
            modelBuilder.Entity<WorldFriend>().HasKey(e => new { e.Id });
            modelBuilder.Entity<WorldComplication>().HasKey(e => new { e.Id });
            modelBuilder.Entity<WorldPlace>().HasKey(e => new { e.Id });
            modelBuilder.Entity<WorldThing>().HasKey(e => new { e.Id });

            modelBuilder.Entity<WorldEnemy>()
                .HasOne<Tag>()
                .WithMany()
                .HasForeignKey(we => we.TagId)
                .IsRequired();

            modelBuilder.Entity<WorldFriend>()
                .HasOne<Tag>()
                .WithMany()
                .HasForeignKey(wf => wf.TagId)
                .IsRequired();

            modelBuilder.Entity<WorldComplication>()
                .HasOne<Tag>()
                .WithMany()
                .HasForeignKey(wc => wc.TagId)
                .IsRequired();

            modelBuilder.Entity<WorldPlace>()
                .HasOne<Tag>()
                .WithMany()
                .HasForeignKey(wp => wp.TagId)
                .IsRequired();

            modelBuilder.Entity<WorldThing>()
                .HasOne<Tag>()
                .WithMany()
                .HasForeignKey(wt => wt.TagId)
                .IsRequired();
        }

        private void ShipModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ship>().HasKey(e => new { e.Id });
            modelBuilder.Entity<Hull>().HasKey(e => new { e.Id });
            modelBuilder.Entity<ShipArmament>().HasKey(e => new { e.Id });
            modelBuilder.Entity<ShipDefense>().HasKey(e => new { e.Id });
            modelBuilder.Entity<ShipFitting>().HasKey(e => new { e.Id });
            modelBuilder.Entity<Spec>().HasKey(e => new { e.Id });
            modelBuilder.Entity<SpecArmament>().HasKey(e => new { e.Id });
            modelBuilder.Entity<SpecDefense>().HasKey(e => new { e.Id });
            modelBuilder.Entity<SpecFitting>().HasKey(e => new { e.Id });

            // SHIPS
            modelBuilder.Entity<Ship>()
                .HasOne<Universe>()
                .WithMany()
                .HasForeignKey(sh => sh.UniverseId)
                .IsRequired();

            modelBuilder.Entity<Ship>()
                .HasOne<Hull>()
                .WithMany()
                .HasForeignKey(sh => sh.HullId)
                .IsRequired();

            // Hulls
            modelBuilder
                .Entity<Hull>()
                .Property(h => h.HullType)
                .HasConversion(new EnumToStringConverter<Hull.HullTypeEnum>());

            modelBuilder
                .Entity<Hull>()
                .Property(h => h.HullClass)
                .HasConversion(new EnumToStringConverter<Hull.HullClassEnum>());

            // SHIP ARMAMENT
            modelBuilder.Entity<ShipArmament>()
                .HasOne<Ship>()
                .WithMany();

            modelBuilder.Entity<ShipArmament>()
                .HasOne<Armament>()
                .WithMany();

            // SHIP DEFENSE
            modelBuilder.Entity<ShipDefense>()
                .HasOne<Ship>()
                .WithMany();

            modelBuilder.Entity<ShipDefense>()
                .HasOne<Defense>()
                .WithMany();

            // SHIP FITTING
            modelBuilder.Entity<ShipFitting>()
                .HasOne<Ship>()
                .WithMany();

            modelBuilder.Entity<ShipFitting>()
                .HasOne<Fitting>()
                .WithMany();

            // SPEC
            modelBuilder.Entity<Spec>()
                .HasOne<Hull>()
                .WithMany()
                .HasForeignKey(s => s.HullId);

            // SPEC ARMAMENT
            modelBuilder.Entity<SpecArmament>()
                .HasOne<Spec>()
                .WithMany();

            modelBuilder.Entity<SpecArmament>()
                .HasOne<Armament>()
                .WithMany();

            // SPEC DEFENSE
            modelBuilder.Entity<SpecDefense>()
                .HasOne<Spec>()
                .WithMany();

            modelBuilder.Entity<SpecDefense>()
                .HasOne<Defense>()
                .WithMany();

            // SPEC FITTING
            modelBuilder.Entity<SpecFitting>()
                .HasOne<Spec>()
                .WithMany();

            modelBuilder.Entity<SpecFitting>()
                .HasOne<Fitting>()
                .WithMany();
        }

        private void CharacterModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().HasKey(e => new { e.Id });
            modelBuilder.Entity<CrewMember>().HasKey(e => new { e.Id });

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
                .WithMany()
                .HasForeignKey(c => c.CharacterId);

            modelBuilder.Entity<CrewMember>()
                .HasOne<Ship>()
                .WithMany();
        }

        private void ProblemModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProblemConflictSituations>().HasKey(t => new { t.Situation });
            modelBuilder.Entity<ProblemConflictFocuses>().HasKey(t => new { t.Focus });
            modelBuilder.Entity<ProblemRestraints>().HasKey(t => new { t.Restraint });
            ;
            modelBuilder.Entity<ProblemTwists>().HasKey(t => new { t.Twist });
        }

        private void PoiModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PointOfInterest>().HasKey(e => new { e.Id });
            modelBuilder.Entity<PoiType>().HasKey(e => new { e.Id });
            modelBuilder.Entity<PoiOccupiedBy>().HasKey(e => new { e.Id });
            modelBuilder.Entity<PoiSituation>().HasKey(e => new { e.Id });

            modelBuilder.Entity<PointOfInterest>()
                .HasOne<Universe>()
                .WithMany()
                .HasForeignKey(e => e.UniverseId)
                .IsRequired();

            modelBuilder.Entity<PoiOccupiedBy>()
                .HasOne<PoiType>()
                .WithMany()
                .HasForeignKey(t => t.TypeId)
                .IsRequired();

            modelBuilder.Entity<PoiSituation>()
                .HasOne<PoiType>()
                .WithMany()
                .HasForeignKey(t => t.TypeId)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options
                .UseSqlite($"Data Source={DbPath}");
    }
}