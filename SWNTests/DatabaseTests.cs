using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNTests;

[TestClass]
public class DatabaseTests
{

    
    private static readonly Random Rand = new ();

    // /// <summary>
    // /// Runs 1 time prior to all tests for setup 
    // /// </summary>
    // /// <param name="testContext"></param>
    // [ClassInitialize]
    // public static async Task ClassInitialize(TestContext testContext)
    // {
    // }

    // /// <summary>
    // /// Runs 1 time when tests are all complete and used for cleanup tasks
    // /// </summary>
    // [ClassCleanup]
    // public static async Task ClassCleanup()
    // {
    // }
    
    private IConfiguration InitConfiguration()
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .Build();
        return config;
    }


    [TestMethod, TestCategory("DatabaseTest")]
    [DataRow("Test Universe Context", true)]
    [DataRow("Test Universe Context 2", true)]
    public void TestCreate(String universeName, bool cleanup)
    {


        var _context = new UniverseContext(InitConfiguration());
    
        var universeId = TestAddUniverse(universeName, _context, out var ucGet);
        //Select stuff
        var query1 = from ucUniverse in _context.Universes where ucUniverse.Id == ucGet.Id select ucUniverse.Name;
        Assert.AreEqual(query1.First(), universeName);

        var query = from ucUniverse in _context.Universes where ucUniverse.Id == ucGet.Id select ucUniverse;
        Assert.AreEqual(query.First().Id, universeId);
        Assert.AreEqual(query.First().Name, universeName);

        TestAddZones(universeId, _context);
        TestAddPlanets(universeId, _context);
        TestAddCharacter(universeId, _context);

        //Join Query
        var joinQuery =
            from u in _context.Universes
            join c in _context.Characters on u.Id equals c.UniverseId
            where u.Id == ucGet.Id
            select new {u.Name, c.First, c.Last};
        var result = joinQuery.First();
        Assert.AreEqual(result.Name, universeName);
        Assert.IsNotNull(result.First);
        Assert.IsNotNull(result.Last);

        
        
        if (cleanup)
        {
            //Cleanup
            foreach (var character in _context.Characters.Where(c => c.UniverseId == universeId))
                _context.Characters.Remove(character);
            
            foreach (var planet in _context.Planets.Where(c => c.UniverseId == universeId))
                _context.Planets.Remove(planet);
            
            foreach (var zone in _context.Zones.Where(c => c.UniverseId == universeId))
                _context.Zones.Remove(zone);

            _context.Universes.Remove(_context.Universes.First(c => c.Id == universeId));

            _context.SaveChanges();

            //Check Cleanup
            Assert.AreEqual(0, _context.Universes.Count(u => u.Id == universeId));
            Assert.AreEqual(0, _context.Characters.Count(u => u.UniverseId == universeId));
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
                Population = (10000 * i),
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
                Height = 175,
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
        var _context = new UniverseContext(InitConfiguration());
        using (var uc = new Repository<Universe>(_context))
        {
            Dictionary<string, string> addIds = new Dictionary<string, string>();
            List<Universe> universes = new List<Universe>();

            for (int i = 0; i < 50; i++)
            {
                Universe universe = new Universe()
                {
                    Name = _context.Naming.Skip(i).Take(1).First().Name,
                    GridX = Rand.Next(5, 10),
                    GridY = Rand.Next(5, 10)
                };
                addIds.Add(universe.Id, universe.Name);
                universes.Add(universe);                    
            }
            //Add Universe Range
            var addResult = uc.AddRange(universes);
            Assert.IsTrue(addResult);

            //Update Universe Range
            universes.Clear();
            Dictionary<string, string> updateIds = new Dictionary<string, string>();
            foreach (var id in addIds)
            {
                 Universe universe = uc.GetById(id.Key);
                 Assert.AreEqual(universe.Name, id.Value);
                 Assert.AreEqual(universe.Id, id.Key);
 
                 universe.Name = _context.Naming.Skip(Rand.Next(1,25)).Take(1).First().Name;
                 updateIds.Add(universe.Id, universe.Name);
                 universes.Add(universe);
            }
            var updateResult = uc.UpdateRange(universes);
            Assert.IsTrue(updateResult);
            
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
            var deleteResult = uc.DeleteRange(universes);
            Assert.IsTrue(deleteResult);
            foreach (var id in updateIds)
            {
                Universe unDelete = uc.GetById(id.Key);
                Assert.IsNull(unDelete);
            }
        }
    }
}