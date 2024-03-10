﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWNUniverseGenerator.CreationTools;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

namespace SWNTests;

[TestClass]
public class CreationTests
{
    /// <summary>
    /// Test the creation of the full universe.
    /// WARNING: This may result in incomplete coverage as it will heavily rely on the randomization of generation
    /// as opposed to easily assertable values. This is largely to ensure the database works as intended.
    /// </summary>
    /// <param name="universeName"></param>
    /// <param name="cleanup"></param>
    [TestMethod, TestCategory("DatabaseTest")]
    [DataRow("Test Full Creation", true)]
    public void TestFullCreation(String universeName, Boolean cleanup)
    {
        using var context = new UniverseContext();
        Creation creation = new Creation();

        string universeId = creation.CreateUniverse(new UniverseDefaultSettings {Name = universeName}, context);

        creation.CreateStars(universeId, new StarDefaultSettings());
        creation.CreatePlanets(universeId, new PlanetDefaultSettings());
        creation.CreateCharacter(universeId, new CharacterDefaultSettings());
        creation.CreateShips(universeId, new ShipDefaultSettings());
        creation.CreateProblems(universeId, new ProblemDefaultSettings());

        Assert.IsTrue(context.Universes.Count(u => u.Id == universeId) > 0);
        Assert.IsTrue(context.Universes.Single(u => u.Id == universeId).GridX == 8);
        Assert.IsTrue(context.Universes.Single(u => u.Id == universeId).GridY == 10);
        Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) > 0);
        Assert.IsTrue(context.Stars.Count(s => s.UniverseId == universeId) > 0);
        Assert.IsTrue(context.Planets.Count(s => s.UniverseId == universeId) > 0);
        Assert.IsTrue(context.Characters.Count(s => s.UniverseId == universeId) > 0);
        Assert.IsTrue(context.Ships.Count(s => s.UniverseId == universeId) > 0);

        // var starMapPath = creation.CreateStarMap(universeId);

        // Assert.IsTrue(File.Exists(starMapPath));

        if (cleanup)
        {
            creation.DeleteUniverse(universeId);

            Assert.IsTrue(context.Universes.Count(u => u.Id == universeId) == 0);
            Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) == 0);
            Assert.IsTrue(context.Stars.Count(s => s.UniverseId == universeId) == 0);
            Assert.IsTrue(context.Planets.Count(s => s.UniverseId == universeId) == 0);
            Assert.IsTrue(context.Characters.Count(c => c.UniverseId == universeId) == 0);
            Assert.IsTrue(context.Ships.Count(s => s.UniverseId == universeId) == 0);
            // Assert.IsFalse(File.Exists(starMapPath));
        }
    }

    /// <summary>
    /// Test the creation of Stars and all the values they can have
    /// </summary>
    /// <param name="universeName"></param>
    /// <param name="cleanup"></param>
    [TestMethod, TestCategory("DatabaseTest")]
    [DataRow("Test Star Class Creation", true)]
    public void TestStarClassCreation(String universeName, Boolean cleanup)
    {
        using var context = new UniverseContext();
        Creation creation = new Creation();

        string universeId = creation.CreateUniverse(new UniverseDefaultSettings {Name = universeName}, context);

        creation.CreateStars(universeId, new StarDefaultSettings
        {
            StarCount = 1,
            StarClass = Star.StarClassEnum.A,
            StarColor = Star.StarColorEnum.Blue
        });
        creation.CreateStars(universeId, new StarDefaultSettings
        {
            StarCount = 1,
            StarClass = Star.StarClassEnum.B,
            StarColor = Star.StarColorEnum.BlueWhite
        });
        creation.CreateStars(universeId, new StarDefaultSettings
        {
            StarCount = 1,
            StarClass = Star.StarClassEnum.F,
            StarColor = Star.StarColorEnum.White
        });
        creation.CreateStars(universeId, new StarDefaultSettings
        {
            StarCount = 1,
            StarClass = Star.StarClassEnum.G,
            StarColor = Star.StarColorEnum.YellowWhite
        });
        creation.CreateStars(universeId, new StarDefaultSettings
        {
            StarCount = 1,
            StarClass = Star.StarClassEnum.M,
            StarColor = Star.StarColorEnum.Yellow
        });
        creation.CreateStars(universeId, new StarDefaultSettings
        {
            StarCount = 1,
            StarClass = Star.StarClassEnum.K,
            StarColor = Star.StarColorEnum.LightOrange
        });
        creation.CreateStars(universeId, new StarDefaultSettings
        {
            StarCount = 1,
            StarClass = Star.StarClassEnum.O,
            StarColor = Star.StarColorEnum.OrangeRed
        });

        creation.CreatePlanets(universeId, new PlanetDefaultSettings());
            
        List<Star> stars = context.Stars.Where(s => s.UniverseId == universeId).ToList();
            
        Assert.IsTrue(context.Universes.Count(u => u.Id == universeId) > 0);
        Assert.IsTrue(context.Zones.Count(z => z.UniverseId == universeId) > 0);
        Assert.IsTrue(context.Planets.Count(p => p.UniverseId == universeId) > 0);
        Assert.IsTrue(stars.Count == 7);
            
        // Classes
        Assert.IsTrue(stars.Count(s => s.StarClass == Star.StarClassEnum.A) == 1);
        Assert.IsTrue(stars.Count(s => s.StarClass == Star.StarClassEnum.M) == 1);
        Assert.IsTrue(stars.Count(s => s.StarClass == Star.StarClassEnum.G) == 1);
        Assert.IsTrue(stars.Count(s => s.StarClass == Star.StarClassEnum.K) == 1);
        Assert.IsTrue(stars.Count(s => s.StarClass == Star.StarClassEnum.B) == 1);
        Assert.IsTrue(stars.Count(s => s.StarClass == Star.StarClassEnum.F) == 1);
        Assert.IsTrue(stars.Count(s => s.StarClass == Star.StarClassEnum.O) == 1);
            
        // Colors
        Assert.IsTrue(stars.Count(s => s.StarColor == Star.StarColorEnum.Blue) == 1);
        Assert.IsTrue(stars.Count(s => s.StarColor == Star.StarColorEnum.BlueWhite) == 1);
        Assert.IsTrue(stars.Count(s => s.StarColor == Star.StarColorEnum.White) == 1);
        Assert.IsTrue(stars.Count(s => s.StarColor == Star.StarColorEnum.YellowWhite) == 1);
        Assert.IsTrue(stars.Count(s => s.StarColor == Star.StarColorEnum.Yellow) == 1);
        Assert.IsTrue(stars.Count(s => s.StarColor == Star.StarColorEnum.LightOrange) == 1);
        Assert.IsTrue(stars.Count(s => s.StarColor == Star.StarColorEnum.OrangeRed) == 1);
            
        Assert.IsTrue(stars.First().UniverseId == universeId);

        // var starMapPath = creation.CreateStarMap(universeId);

        // Assert.IsTrue(File.Exists(starMapPath));

        if (cleanup)
        {
            creation.DeleteUniverse(universeId);

            Assert.IsTrue(context.Universes.Count(u => u.Id == universeId) == 0);
            Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) == 0);
            Assert.IsTrue(context.Stars.Count(s => s.UniverseId == universeId) == 0);
            Assert.IsTrue(context.Planets.Count(s => s.UniverseId == universeId) == 0);
            // Assert.IsFalse(File.Exists(starMapPath));
        }
    }

    /// <summary>
    /// Test the creation of a grid with a width of 20x20
    /// </summary>
    /// <param name="universeName"></param>
    /// <param name="cleanup"></param>
    [TestMethod, TestCategory("DatabaseTest")]
    [DataRow("Test Wide Grid Creation", true)]
    public void TestWideGridCreation(String universeName, Boolean cleanup)
    {
        using var context = new UniverseContext();
        Creation creation = new Creation();

        string universeId =
            creation.CreateUniverse(new UniverseDefaultSettings
                {
                    Name = universeName,
                    GridX = 20,
                    GridY = 20
                },
                context);

        Assert.IsTrue(context.Universes.Count(u => u.Id == universeId) > 0);
        Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) > 0);
        Assert.IsTrue(context.Universes.Single(u => u.Id == universeId).GridX == 20);
        Assert.IsTrue(context.Universes.Single(u => u.Id == universeId).GridY == 20);

        // var starMapPath = creation.CreateStarMap(universeId);

        // Assert.IsTrue(File.Exists(starMapPath));

        if (cleanup)
        {
            creation.DeleteUniverse(universeId);

            Assert.IsTrue(context.Universes.Count(u => u.Id == universeId) == 0);
            Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) == 0);
            // Assert.IsFalse(File.Exists(starMapPath));
        }
    }
    
    /// <summary>
    /// Test the creation of Planets and all the values they can have
    /// </summary>
    /// <param name="universeName"></param>
    /// <param name="cleanup"></param>
    [TestMethod, TestCategory("DatabaseTest")]
    [DataRow("Test Planet Class Creation", true)]
    public void TestPlanetClassCreation(String universeName, Boolean cleanup)
    {
        using var context = new UniverseContext();
        Creation creation = new Creation();

        string universeId =
            creation.CreateUniverse(new UniverseDefaultSettings {Name = universeName}, context);

        creation.CreateStars(universeId, new StarDefaultSettings());

        creation.CreatePlanets(universeId, new PlanetDefaultSettings());

        Assert.IsTrue(context.Universes.Count(u => u.Id == universeId) > 0);
        Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) > 0);
        Assert.IsTrue(context.Stars.Count(s => s.UniverseId == universeId) > 0);
        Assert.IsTrue(context.Planets.Count(s => s.UniverseId == universeId) > 0);
        Assert.IsTrue(context.Planets.First(s => s.UniverseId == universeId).UniverseId == universeId);

        // var starMapPath = creation.CreateStarMap(universeId);

        // Assert.IsTrue(File.Exists(starMapPath));

        if (cleanup)
        {
            creation.DeleteUniverse(universeId);

            Assert.IsTrue(context.Universes.Count(u => u.Id == universeId) == 0);
            Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) == 0);
            Assert.IsTrue(context.Stars.Count(s => s.UniverseId == universeId) == 0);
            Assert.IsTrue(context.Planets.Count(s => s.UniverseId == universeId) == 0);
            // Assert.IsFalse(File.Exists(starMapPath));
        }
    }

    /// <summary>
    /// Test the creation of characters and all the values a Character can have
    /// </summary>
    /// <param name="universeName"></param>
    /// <param name="cleanup"></param>
    [TestMethod, TestCategory("DatabaseTest")]
    [DataRow("Test Character Creation", true)]
    public void TestCharacterCreation(String universeName, Boolean cleanup)
    {
        using var context = new UniverseContext();
        Creation creation = new Creation();

        string universeId =
            creation.CreateUniverse(new UniverseDefaultSettings {Name = universeName}, context);

        creation.CreateStars(universeId, new StarDefaultSettings());
        creation.CreatePlanets(universeId, new PlanetDefaultSettings());
            
        creation.CreateCharacter(universeId, new CharacterDefaultSettings
        {
            Count = 1,
            Gender = Character.GenderEnum.Male,
            First = "John",
            Last = "Doe",
            Age = 25,
            HairStyle = "Long Straight",
            HairCol = "Brown",
            EyeCol = "Blue",
            SkinCol = "Pale",
            Title = "Dude",
            Height = 120,
            CurrentPlanetId = context.Planets.First(p => p.UniverseId == universeId).Id,
            CrimeChance = new[]{50, 50},
            InitialReaction = "Warm"
        });
        creation.CreateCharacter(universeId, new CharacterDefaultSettings
        {
            Count = 1,
            Gender = Character.GenderEnum.Female,
            First = "Jane",
            Last = "Doe",
        });

        Assert.IsTrue(context.Universes.Count(u => u.Id == universeId) > 0);
        Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) > 0);
        Assert.IsTrue(context.Stars.Count(s => s.UniverseId == universeId) > 0);
        Assert.IsTrue(context.Planets.Count(s => s.UniverseId == universeId) > 0);

        List<Character> chars = context.Characters.Where(c => c.UniverseId == universeId).ToList();
            
        Assert.IsTrue(context.Characters.Count(c => c.UniverseId == universeId) == 2);
        Assert.IsTrue(chars.Count == 2);
        Assert.IsTrue(chars.First().First == "John");
        Assert.IsTrue(chars.First().Last == "Doe");
        Assert.IsTrue(chars.First().Name == "John Doe");
        Assert.IsTrue(chars.First().Age == 25);
        Assert.IsTrue(chars.First().HairStyle == "Long Straight");
        Assert.IsTrue(chars.First().HairCol == "Brown");
        Assert.IsTrue(chars.First().EyeCol == "Blue"); 
        Assert.IsTrue(chars.First().Title == "Dude");
        Assert.IsTrue(chars.First().SkinCol == "Pale");
        Assert.IsTrue(chars.First().Height == 120);
        Assert.IsTrue(chars.First().Gender == Character.GenderEnum.Male);
        Assert.IsTrue(chars.First().CrimeChance == 50);
        Assert.IsTrue(chars.First().InitialReaction == "Warm");
        Assert.IsTrue(chars.First().CurrentLocationId == context.Planets.First(p => p.UniverseId == universeId).Id);
            
        Assert.IsTrue(chars.Last().First == "Jane");
        Assert.IsTrue(chars.Last().Last == "Doe");
        Assert.IsTrue(chars.Last().Gender == Character.GenderEnum.Female);
            
        Assert.IsTrue(chars.First().UniverseId == universeId);

        // var starMapPath = creation.CreateStarMap(universeId);

        // Assert.IsTrue(File.Exists(starMapPath));

        if (cleanup)
        {
            creation.DeleteUniverse(universeId);

            Assert.IsTrue(context.Universes.Count(u => u.Id == universeId) == 0);
            Assert.IsTrue(context.Zones.Count(s => s.UniverseId == universeId) == 0);
            Assert.IsTrue(context.Stars.Count(s => s.UniverseId == universeId) == 0);
            Assert.IsTrue(context.Planets.Count(s => s.UniverseId == universeId) == 0);
            // Assert.IsFalse(File.Exists(starMapPath));
        }
    }
}