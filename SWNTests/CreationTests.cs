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
                    foreach (var star in context.Stars.Where(s => s.UniverseId == universeId))
                        starRepo.Delete(star);
                

                using (var planRepo = new Repository<Planet>(context))
                    foreach (var planet in context.Planets.Where(s => s.UniverseId == universeId))
                        planRepo.Delete(planet);

                using (var zoneRepo = new Repository<Zone>(context))
                    foreach (var zone in context.Zones.Where(z => z.UniverseId == universeId))
                        zoneRepo.Delete(zone);

                using (var uniRepo = new Repository<Universe>(context))
                    uniRepo.Delete(universeId);
                
                using (var charRepo = new Repository<Character>(context))
                    foreach (var character in context.Characters.Where(c => c.UniverseId == universeId))
                        charRepo.Delete(character);
                
                using (var shipRepo = new Repository<Ship>(context))
                    foreach (var ship in context.Ships.Where(c => c.UniverseId == universeId))
                        shipRepo.Delete(ship);
                
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