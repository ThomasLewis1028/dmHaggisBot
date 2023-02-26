using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWNUniverseGenerator;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNTests;

[TestClass]
public class DatabaseTests
{
    private static readonly Random Rand = new Random();
    
    /// <summary>
    /// Runs 1 time prior to all tests for setup 
    /// </summary>
    /// <param name="testContext"></param>
    [ClassInitialize]
    public static async Task ClassInitialize(TestContext testContext)
    {
        
    }

    /// <summary>
    /// Runs 1 time when tests are all complete and used for cleanup tasks
    /// </summary>
    [ClassCleanup]
    public static async Task ClassCleanup()
    {
    }
    
    
    [TestMethod, TestCategory("DatabaseTest")]
    [DataRow ("TestUniverse", "Bob", "Bobber", false)]
    [DataRow ("Other Test Universe", "Ron", "Burgundy", false)]
    public void TestCreate(String universeName, String first, String last, bool cleanup)
    {
        using (var uc = new UniverseContext())
        {
            //Add Universe
            Universe universe = new Universe()
            {
                Name = universeName,
                GridX = Rand.Next(5, 10),
                GridY = Rand.Next(5, 10)
            };
            //IdGen.GenerateId(universe);
            var universeId = universe.Id;
            uc.Universes.Add(universe);
            uc.SaveChanges();

            Universe ucGet = uc.Universes.First(u => u.Id == universeId);
            Assert.AreEqual(ucGet.Name, universe.Name);
            Assert.AreEqual(ucGet.Id, universeId);

            //Add Characters
            for (int i = 0; i < 3; i++)
            {
                var c = new Character()
                {
                    UniverseId = ucGet.Id,
                    Age = 25 + i,
                    Title = "Blah Blah",
                    First = first,
                    Last = last
                };
                uc.Characters.Add(c);
            }

            uc.SaveChanges();

            //Check Character Create
            var charCount = uc.Characters.Count(c => c.UniverseId == ucGet.Id);
            Assert.AreEqual(3, charCount);
            var ageCount = uc.Characters.Count(c => c.UniverseId == ucGet.Id && c.Age == 26);
            Assert.AreEqual(1, ageCount);

            //Select stuff
            var query1 = from ucUniverse in uc.Universes where ucUniverse.Id == ucGet.Id select ucUniverse.Name;
            Assert.AreEqual(query1.First(), universeName);
            
            var query = from ucUniverse in uc.Universes where ucUniverse.Id == ucGet.Id select ucUniverse;
            Assert.AreEqual(query.First().Id, universeId);
            Assert.AreEqual(query.First().Name, universeName);
            
            //Join Query
            var joinquery = 
                from u in uc.Universes
                join c in uc.Characters on u.Id equals c.UniverseId 
                where u.Id == ucGet.Id select new { u.Name, c.First, c.Last };
            var result = joinquery.First();
            Assert.AreEqual(result.Name, universeName);
            Assert.AreEqual(result.First, first);
            Assert.AreEqual(result.Last, last);

            if (cleanup)
            {
                //Cleanup
                foreach (var character in uc.Characters.Where(c => c.UniverseId == universeId))
                {
                    uc.Characters.Remove(character);
                }

                uc.Universes.Remove(uc.Universes.First(c => c.Id == universeId));

                uc.SaveChanges();

                //Check Cleanup
                Assert.AreEqual(0, uc.Universes.Count(u => u.Id == universeId));
                Assert.AreEqual(0, uc.Characters.Count(u => u.UniverseId == universeId));
            }
        }

    }
}