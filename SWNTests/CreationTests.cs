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

            string universeId = creation.CreateUniverse(new UniverseDefaultSettings(){Name = universeName}, context);

            creation.CreateStars(universeId, new StarDefaultSettings());
            creation.CreatePlanets(universeId, new PlanetDefaultSettings());
            creation.CreateCharacter(universeId, new CharacterDefaultSettings());
            creation.CreateShips(universeId, new ShipDefaultSettings());
            
            Assert.IsTrue(context.Universes.Count(u => u.Id == universeId ) > 0);
            Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) > 0);
            Assert.IsTrue(context.Stars.Count(s => s.UniverseId == universeId) > 0);
            Assert.IsTrue(context.Planets.Count(s => s.UniverseId == universeId) > 0);
            Assert.IsTrue(context.Characters.Count(s => s.UniverseId == universeId) > 0);
            Assert.IsTrue(context.Ships.Count(s => s.UniverseId == universeId) > 0);

            if (cleanup)
            {
                using (var starRepo = new Repository<Star>(context))
                    starRepo.DeleteRange(context.Stars.Where(c => c.UniverseId == universeId).ToList());

                using (var planRepo = new Repository<Planet>(context))
                    planRepo.DeleteRange(context.Planets.Where(c => c.UniverseId == universeId).ToList());

                using (var zoneRepo = new Repository<Zone>(context))
                    zoneRepo.DeleteRange(context.Zones.Where(c => c.UniverseId == universeId).ToList());

                using (var uniRepo = new Repository<Universe>(context))
                    uniRepo.Delete(universeId);

                using (var charRepo = new Repository<Character>(context))
                    charRepo.DeleteRange(context.Characters.Where(c => c.UniverseId == universeId).ToList());
                
                using (var shipRepo = new Repository<Ship>(context))
                    shipRepo.DeleteRange(context.Ships.Where(c => c.UniverseId == universeId).ToList());

                Assert.IsTrue(context.Universes.Count(u => u.Id == universeId ) == 0);
                Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) == 0);
                Assert.IsTrue(context.Stars.Count(s => s.UniverseId == universeId) == 0);
                Assert.IsTrue(context.Planets.Count(s => s.UniverseId == universeId) == 0);
                Assert.IsTrue(context.Characters.Count(c => c.UniverseId == universeId) == 0);
                Assert.IsTrue(context.Ships.Count(s => s.UniverseId == universeId) == 0);
            }
        }
        
    }
}