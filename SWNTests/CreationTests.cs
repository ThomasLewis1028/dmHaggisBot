using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWNUniverseGenerator;
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

            string universeId = "";

            universeId =
                creation.CreateUniverse(new UniverseDefaultSettings() {Name = universeName}, context);

            creation.CreateStars(universeId, new StarDefaultSettings());
            creation.CreatePlanets(universeId, new PlanetDefaultSettings());
            creation.CreateCharacter(universeId, new CharacterDefaultSettings());
            creation.CreateShips(universeId, new ShipDefaultSettings());

            Assert.IsTrue(context.Universes.Count(u => u.Id == universeId) > 0);
            Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) > 0);
            Assert.IsTrue(context.Stars.Count(s => s.UniverseId == universeId) > 0);
            Assert.IsTrue(context.Planets.Count(s => s.UniverseId == universeId) > 0);
            Assert.IsTrue(context.Characters.Count(s => s.UniverseId == universeId) > 0);
            Assert.IsTrue(context.Ships.Count(s => s.UniverseId == universeId) > 0);


            if (cleanup)
            {
                creation.DeleteUniverse(universeId);

                Assert.IsTrue(context.Universes.Count(u => u.Id == universeId) == 0);
                Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) == 0);
                Assert.IsTrue(context.Stars.Count(s => s.UniverseId == universeId) == 0);
                Assert.IsTrue(context.Planets.Count(s => s.UniverseId == universeId) == 0);
                Assert.IsTrue(context.Characters.Count(c => c.UniverseId == universeId) == 0);
                Assert.IsTrue(context.Ships.Count(s => s.UniverseId == universeId) == 0);
            }
        }
    }
}