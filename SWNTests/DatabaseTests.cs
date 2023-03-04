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
    [DataRow("Test Universe Context", true)]
    [DataRow("Test Universe Context 2", true)]
    public void TestCreate(String universeName, bool cleanup)
    {
        using (var context = new UniverseContext())
        {
            var universeId = TestAddUniverse(universeName, context, out var ucGet);
            //Select stuff
            var query1 = from ucUniverse in context.Universes where ucUniverse.Id == ucGet.Id select ucUniverse.Name;
            Assert.AreEqual(query1.First(), universeName);

            var query = from ucUniverse in context.Universes where ucUniverse.Id == ucGet.Id select ucUniverse;
            Assert.AreEqual(query.First().Id, universeId);
            Assert.AreEqual(query.First().Name, universeName);

            TestAddZones(universeId, context);
            TestAddPlanets(universeId, context);
            TestAddCharacter(universeId, context);

            //Join Query
            var joinquery =
                from u in context.Universes
                join c in context.Characters on u.Id equals c.UniverseId
                where u.Id == ucGet.Id
                select new {u.Name, c.First, c.Last};
            var result = joinquery.First();
            Assert.AreEqual(result.Name, universeName);
            Assert.IsNotNull(result.First);
            Assert.IsNotNull(result.Last);

            
            
            if (cleanup)
            {
                //Cleanup
                foreach (var character in context.Characters.Where(c => c.UniverseId == universeId))
                {
                    context.Characters.Remove(character);
                }
                
                foreach (var planet in context.Planets.Where(c => c.UniverseId == universeId))
                {
                    context.Planets.Remove(planet);
                }
                
                foreach (var zone in context.Zones.Where(c => c.UniverseId == universeId))
                {
                    context.Zones.Remove(zone);
                }

                context.Universes.Remove(context.Universes.First(c => c.Id == universeId));

                context.SaveChanges();

                //Check Cleanup
                Assert.AreEqual(0, context.Universes.Count(u => u.Id == universeId));
                Assert.AreEqual(0, context.Characters.Count(u => u.UniverseId == universeId));
            }
        }
    }

    private static string TestAddUniverse(string universeName, UniverseContext context, out Universe ucGet)
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
        context.Universes.Add(universe);
        context.SaveChanges();

        Assert.IsNotNull(universeId);        
        ucGet = context.Universes.First(u => u.Id == universeId);
        Assert.AreEqual(ucGet.Name, universe.Name);
        Assert.AreEqual(ucGet.Id, universeId);

        
        return universeId;
    }

    private static void TestAddZones(String universeId, UniverseContext context)
    {
        //Add Zones
        for (int i = 0; i < 3; i++)
        {
            var z = new Zone()
            {
                UniverseId = universeId,
                X = 8,
                Y = 10
            };
            context.Zones.Add(z);
        }

        context.SaveChanges();
        
        //Check Zone Create
        var zoneCount = context.Zones.Count(c => c.UniverseId == universeId);
        Assert.AreEqual(3, zoneCount);
    }
    
    private static void TestAddPlanets(String universeId, UniverseContext context)
    {
        //Add Zones
        for (int i = 0; i < 3; i++)
        {
            var p = new Planet()
            {
                UniverseId = universeId,
                Name = context.Naming.Where(n => n.NameType == "Planet").Skip(i).Take(1).First().Name,
                Atmosphere = "oxygen",
                Biosphere = "Clouds",
                Contact = "Bob",
                Origin = "God",
                Population = (10000 * i).ToString(),
                Relationship = "It's Complicated",
                Temperature = "HOT",
                IsPrimary = false,
                TechLevel = "Stone Age",
                ZoneId = context.Zones.Skip(i).Take(1).First().Id,
                FirstWorldTag = null,
                SecondWorldTag = null

            };
            context.Planets.Add(p);
        }

        context.SaveChanges();
        
        //Check Zone Create
        var planetCount = context.Planets.Count(c => c.UniverseId == universeId);
        Assert.AreEqual(3, planetCount);
    }
    
    private static void TestAddCharacter(String universeId, UniverseContext context)
    {
        //Add Zones
        for (int i = 0; i < 3; i++)
        {
            var c = new Character()
            {
                UniverseId = universeId,
                First = context.Naming.Where(n => n.NameType == "MaleName").Skip(i).Take(1).First().Name,
                Last = context.Naming.Where(n => n.NameType == "LastName").Skip(i).Take(1).First().Name,
                Age = 25+i,
                Gender = Character.GenderEnum.Male,
                Height = "7.0",
                Title = context.Naming.Where(n => n.NameType == "Noun").Skip(i).Take(1).First().Name,
                CrimeChance = i,
                EyeCol = context.Naming.Where(n => n.NameType == "EyeColor").Skip(i).Take(1).First().Name,
                HairCol = context.Naming.Where(n => n.NameType == "HairColor").Skip(i).Take(1).First().Name,
                HairStyle = context.Naming.Where(n => n.NameType == "HairStyle").Skip(i).Take(1).First().Name,
                InitialReaction = "",
                SkinCol = context.Naming.Where(n => n.NameType == "EyeColor").Skip(i).Take(1).First().Name,
                CurrentLocationId = context.Planets.Skip(i).Take(1).First().Id,
                BirthPlanetId = context.Planets.Skip(i).Take(1).First().Id
            };
            context.Characters.Add(c);
        }

        context.SaveChanges();
        
        //Check Zone Create
        var charCount = context.Characters.Count(c => c.UniverseId == universeId);
        Assert.AreEqual(3, charCount);
    }

    [TestMethod, TestCategory("DatabaseTest")]
    public void TestRepositoryCreate()
    {
        using (var context = new UniverseContext())
        {
            using (var uc = new Repository<Universe>(context))
            {
                Dictionary<string, string> addIds = new Dictionary<string, string>();
                List<Universe> universes = new List<Universe>();

                for (int i = 0; i < 3; i++)
                {
                    Universe universe = new Universe()
                    {
                        Name = context.Naming.Skip(i).Take(1).First().Name,
                        GridX = Rand.Next(5, 10),
                        GridY = Rand.Next(5, 10)
                    };
                    addIds.Add(universe.Id, universe.Name);
                    universes.Add(universe);                    
                }
                //Add Universe Range
                uc.AddRange(universes);

                //Update Universe Range
                universes.Clear();
                Dictionary<string, string> updateIds = new Dictionary<string, string>();
                foreach (var id in addIds)
                {
                     Universe universe = uc.GetById(id.Key);
                     Assert.AreEqual(universe.Name, id.Value);
                     Assert.AreEqual(universe.Id, id.Key);
     
                     universe.Name = context.Naming.Skip(Rand.Next(1,25)).Take(1).First().Name;
                     updateIds.Add(universe.Id, universe.Name);
                     universes.Add(universe);
                }
                uc.UpdateRange(universes);
                
                //Check Update Universe Range
                universes.Clear();
                foreach (var id in updateIds)
                {
                    Universe universe = uc.GetById(id.Key);
                    Assert.AreEqual(universe.Name, id.Value);
                    Assert.AreEqual(universe.Id, id.Key);
                    
                    universes.Add(universe);
                }
                
                //Delete Universe Range
                uc.DeleteRange(universes);
                foreach (var id in updateIds)
                {
                    Universe unDelete = uc.GetById(id.Key);
                    Assert.IsNull(unDelete);
                }
            }
        }
    }
}