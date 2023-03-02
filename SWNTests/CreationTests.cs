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
    [DataRow("Test Full Creation", false)]
    public void TestFullCreation(String universeName, Boolean cleanup)
    {
        using (var context = new UniverseContext())
        {
            Creation creation = new Creation();

            string universeId = creation.CreateUniverse(new UniverseDefaultSettings(){Name = universeName}, context);

            creation.CreateStars(universeId, new StarDefaultSettings());
            creation.CreatePlanets(universeId, new PlanetDefaultSettings());
            
            Assert.IsTrue(context.Stars.Count(s => s.UniverseId == universeId) > 0);
            Assert.IsTrue(context.Planets.Count(s => s.UniverseId == universeId) > 0);

            if (cleanup)
            {
                using (var starRepo = new Repository<Star>(context))
                {
                    starRepo.DeleteRange(context.Stars.Where(s => s.UniverseId == universeId).ToList());
                }
                
                using (var uniRepo = new Repository<Universe>(context))
                {
                    uniRepo.Delete(universeId);
                }
            }
        }
        
    }
}