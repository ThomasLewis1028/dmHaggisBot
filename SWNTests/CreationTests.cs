using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWNUniverseGenerator;
using SWNUniverseGenerator.CreationTools;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

namespace SWNTests;

[TestClass]
public class CreationTests
{
    [TestMethod, TestCategory("DatabaseTest")]
    [DataRow("Test Full Creation", true)]
    public void TestFullCreation(String universeName, Boolean cleanup)
    {
        using (var context = new UniverseContext())
        {
            Creation creation = new Creation();

            string universeId = creation.CreateUniverse(new UniverseDefaultSettings {Name = universeName}, context);

            creation.CreateStars(universeId, new StarDefaultSettings());
            creation.CreatePlanets(universeId, new PlanetDefaultSettings());
            creation.CreateCharacter(universeId, new CharacterDefaultSettings());
            creation.CreateShips(universeId, new ShipDefaultSettings());
            creation.CreateProblems(universeId, new ProblemDefaultSettings());

            Assert.IsTrue(context.Universes.Count(u => u.Id == universeId) > 0);
            Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) > 0);
            Assert.IsTrue(context.Stars.Count(s => s.UniverseId == universeId) > 0);
            Assert.IsTrue(context.Planets.Count(s => s.UniverseId == universeId) > 0);
            Assert.IsTrue(context.Characters.Count(s => s.UniverseId == universeId) > 0);
            Assert.IsTrue(context.Ships.Count(s => s.UniverseId == universeId) > 0);

            var starMapPath = creation.CreateStarMap(universeId);

            Assert.IsTrue(File.Exists(starMapPath));

            if (cleanup)
            {
                creation.DeleteUniverse(universeId);

                Assert.IsTrue(context.Universes.Count(u => u.Id == universeId) == 0);
                Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) == 0);
                Assert.IsTrue(context.Stars.Count(s => s.UniverseId == universeId) == 0);
                Assert.IsTrue(context.Planets.Count(s => s.UniverseId == universeId) == 0);
                Assert.IsTrue(context.Characters.Count(c => c.UniverseId == universeId) == 0);
                Assert.IsTrue(context.Ships.Count(s => s.UniverseId == universeId) == 0);
                Assert.IsFalse(File.Exists(starMapPath));
            }
        }
    }

    [TestMethod, TestCategory("DatabaseTest")]
    [DataRow("Test Star Class Creation", true)]
    public void TestStarClassCreation(String universeName, Boolean cleanup)
    {
        using (var context = new UniverseContext())
        {
            Creation creation = new Creation();

            string universeId = "";

            universeId =
                creation.CreateUniverse(new UniverseDefaultSettings {Name = universeName}, context);

            creation.CreateStars(universeId, new StarDefaultSettings
            {
                StarCount = 1,
                StarClass = Star.StarClassEnum.A
            });
            creation.CreateStars(universeId, new StarDefaultSettings
            {
                StarCount = 1,
                StarClass = Star.StarClassEnum.B
            });
            creation.CreateStars(universeId, new StarDefaultSettings
            {
                StarCount = 1,
                StarClass = Star.StarClassEnum.F
            });
            creation.CreateStars(universeId, new StarDefaultSettings
            {
                StarCount = 1,
                StarClass = Star.StarClassEnum.G
            });
            creation.CreateStars(universeId, new StarDefaultSettings
            {
                StarCount = 1,
                StarClass = Star.StarClassEnum.M
            });
            creation.CreateStars(universeId, new StarDefaultSettings
            {
                StarCount = 1,
                StarClass = Star.StarClassEnum.K
            });
            creation.CreateStars(universeId, new StarDefaultSettings
            {
                StarCount = 1,
                StarClass = Star.StarClassEnum.O
            });

            creation.CreatePlanets(universeId, new PlanetDefaultSettings());

            Assert.IsTrue(context.Universes.Count(u => u.Id == universeId) > 0);
            Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) > 0);
            Assert.IsTrue(context.Stars.Count(s => s.UniverseId == universeId) == 7);
            Assert.IsTrue(context.Planets.Count(s => s.UniverseId == universeId) > 0);

            var starMapPath = creation.CreateStarMap(universeId);

            Assert.IsTrue(File.Exists(starMapPath));

            if (cleanup)
            {
                creation.DeleteUniverse(universeId);

                Assert.IsTrue(context.Universes.Count(u => u.Id == universeId) == 0);
                Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) == 0);
                Assert.IsTrue(context.Stars.Count(s => s.UniverseId == universeId) == 0);
                Assert.IsTrue(context.Planets.Count(s => s.UniverseId == universeId) == 0);
                Assert.IsFalse(File.Exists(starMapPath));
            }
        }
    }

    [TestMethod, TestCategory("DatabaseTest")]
    [DataRow("Test Wide Grid Creation", true)]
    public void TestWideGridCreation(String universeName, Boolean cleanup)
    {
        using (var context = new UniverseContext())
        {
            Creation creation = new Creation();

            string universeId = "";

            universeId =
                creation.CreateUniverse(new UniverseDefaultSettings
                    {
                        Name = universeName,
                        GridX = 20,
                        GridY = 20
                    },
                    context);

            Assert.IsTrue(context.Universes.Count(u => u.Id == universeId) > 0);
            Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) > 0);

            var starMapPath = creation.CreateStarMap(universeId);

            Assert.IsTrue(File.Exists(starMapPath));

            if (cleanup)
            {
                creation.DeleteUniverse(universeId);

                Assert.IsTrue(context.Universes.Count(u => u.Id == universeId) == 0);
                Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) == 0);
                Assert.IsFalse(File.Exists(starMapPath));
            }
        }
    }
    
    
    [TestMethod, TestCategory("DatabaseTest")]
    [DataRow("Test Planet Class Creation", true)]
    public void TestPlanetClassCreation(String universeName, Boolean cleanup)
    {
        using (var context = new UniverseContext())
        {
            Creation creation = new Creation();

            string universeId = "";

            universeId =
                creation.CreateUniverse(new UniverseDefaultSettings {Name = universeName}, context);

            creation.CreateStars(universeId, new StarDefaultSettings());

            creation.CreatePlanets(universeId, new PlanetDefaultSettings());

            Assert.IsTrue(context.Universes.Count(u => u.Id == universeId) > 0);
            Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) > 0);
            Assert.IsTrue(context.Stars.Count(s => s.UniverseId == universeId) == 7);
            Assert.IsTrue(context.Planets.Count(s => s.UniverseId == universeId) > 0);

            var starMapPath = creation.CreateStarMap(universeId);

            Assert.IsTrue(File.Exists(starMapPath));

            if (cleanup)
            {
                creation.DeleteUniverse(universeId);

                Assert.IsTrue(context.Universes.Count(u => u.Id == universeId) == 0);
                Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) == 0);
                Assert.IsTrue(context.Stars.Count(s => s.UniverseId == universeId) == 0);
                Assert.IsTrue(context.Planets.Count(s => s.UniverseId == universeId) == 0);
                Assert.IsFalse(File.Exists(starMapPath));
            }
        }
    }
}