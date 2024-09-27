using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Test for Roman Numerals
    /// </summary>
    [TestMethod, TestCategory("DatabaseTest")]
    public void TestRomanNumerals()
    {
        Assert.AreEqual(RomanNumerals.ToRoman(3999), "MMMCMXCIX");
        Assert.AreEqual(RomanNumerals.ToRoman(1000), "M");
        Assert.AreEqual(RomanNumerals.ToRoman(900), "CM");
        Assert.AreEqual(RomanNumerals.ToRoman(500), "D");
        Assert.AreEqual(RomanNumerals.ToRoman(400), "CD");
        Assert.AreEqual(RomanNumerals.ToRoman(100), "C");
        Assert.AreEqual(RomanNumerals.ToRoman(90), "XC");
        Assert.AreEqual(RomanNumerals.ToRoman(50), "L");
        Assert.AreEqual(RomanNumerals.ToRoman(40), "XL");
        Assert.AreEqual(RomanNumerals.ToRoman(10), "X");
        Assert.AreEqual(RomanNumerals.ToRoman(9), "IX");
        Assert.AreEqual(RomanNumerals.ToRoman(5), "V");
        Assert.AreEqual(RomanNumerals.ToRoman(4), "IV");
        Assert.AreEqual(RomanNumerals.ToRoman(1), "I");
        Assert.AreEqual(RomanNumerals.ToRoman(1248), "MCCXLVIII");
        Assert.AreEqual(RomanNumerals.ToRoman(999), "CMXCIX");
        Assert.AreEqual(RomanNumerals.ToRoman(0), string.Empty);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => RomanNumerals.ToRoman(4000));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => RomanNumerals.ToRoman(-1));
    }

    /// <summary>
    /// Test the creation of a grid with a width of 20x20
    /// </summary>
    /// <param name="universeName"></param>
    /// <param name="cleanup"></param>
    [TestMethod, TestCategory("DatabaseTest")]
    // [DataRow("Test Grid Creation", false)] // Used for testing
    [DataRow("Test Grid Creation", true)]
    public void TestGridCreation(String universeName, Boolean cleanup)
    {
        using var context = new UniverseContext();
        Creation creation = new Creation();
        UniverseDefaultSettings uds = new UniverseDefaultSettings(
            name: universeName,
            universeId: new Universe().Id);

        creation.CreateUniverse(universeDefaultSettings: uds);
        creation.CreateZones(universeDefaultSettings: uds);

        Assert.IsTrue(context.Universes.Count(u => u.Id == uds.UniverseId) > 0);
        Assert.IsTrue(context.Zones.Count(s => s.UniverseId == uds.UniverseId) > 0);

        var starMapPath = creation.CreateStarMap(uds.UniverseId);
        var starMapPng = new StarMapCreation().GetPng(uds.UniverseId);

        Assert.IsTrue(File.Exists(starMapPath));
        Assert.IsTrue(File.Exists(starMapPng));

        if (cleanup)
        {
            creation.DeleteUniverse(uds.UniverseId);

            Assert.IsTrue(context.Universes.Count(u => u.Id == uds.UniverseId) == 0);
            Assert.IsTrue(context.Zones.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsFalse(File.Exists(starMapPath));
            Assert.IsFalse(File.Exists(starMapPng));
        }
    }

    /// <summary>
    /// Test the creation of a grid with a width of 20x20
    /// </summary>
    /// <param name="universeName"></param>
    /// <param name="cleanup"></param>
    [TestMethod, TestCategory("DatabaseTest")]
    // [DataRow("Test Wide Grid Creation", false)] // Used for testing
    [DataRow("Test Wide Grid Creation", true)]
    public void TestWideGridCreation(String universeName, Boolean cleanup)
    {
        using var context = new UniverseContext();
        Creation creation = new Creation();
        UniverseDefaultSettings uds = new UniverseDefaultSettings
        {
            UniverseId = new Universe().Id,
            Name = universeName,
            GridX = 20,
            GridY = 20
        };

        creation.CreateUniverse(universeDefaultSettings: uds);
        creation.CreateZones(universeDefaultSettings: uds);

        Assert.IsTrue(context.Universes.Count(u => u.Id == uds.UniverseId) > 0);
        Assert.IsTrue(context.Zones.Count(s => s.UniverseId == uds.UniverseId) > 0);
        Assert.IsTrue(context.Universes.Single(u => u.Id == uds.UniverseId).GridX == 20);
        Assert.IsTrue(context.Universes.Single(u => u.Id == uds.UniverseId).GridY == 20);

        var starMapPath = creation.CreateStarMap(uds.UniverseId);

        Assert.IsTrue(File.Exists(starMapPath));

        if (cleanup)
        {
            creation.DeleteUniverse(uds.UniverseId);

            Assert.IsTrue(context.Universes.Count(u => u.Id == uds.UniverseId) == 0);
            Assert.IsTrue(context.Zones.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsFalse(File.Exists(starMapPath));
        }
    }

    /// <summary>
    /// Test the creation of points of interest
    /// </summary>
    /// <param name="universeName"></param>
    /// <param name="cleanup"></param>
    /// 
    [TestMethod, TestCategory("DatabaseTest")]
    // [DataRow("Test Wide Grid Creation", false)] // Used for testing
    [DataRow("Test POI Creation", true)]
    public void TestPoiCreation(String universeName, Boolean cleanup)
    {
        using var context = new UniverseContext();
        Creation creation = new Creation();

        UniverseDefaultSettings uds = new UniverseDefaultSettings()
        {
            Name = universeName,
            UniverseId = new Universe().Id,
        };

        creation.CreateUniverse(universeDefaultSettings: uds);
        creation.CreateZones(universeDefaultSettings: uds);

        creation.CreateStars(new StarDefaultSettings
        {
            UniverseId = uds.UniverseId,
            StarCount = 3
        });

        creation.CreatePlanets(new PlanetDefaultSettings
        {
            UniverseId = uds.UniverseId,
        });

        // Create Star POIs
        creation.CreatePoi(new PoiDefaultSettings
        {
            UniverseId = uds.UniverseId,
            LocationId = context.Stars.Where(s => s.UniverseId == uds.UniverseId).Select(s => s.Id).ToList()
        });

        // Create Planet POIs
        creation.CreatePoi(new PoiDefaultSettings
        {
            UniverseId = uds.UniverseId,
            LocationId = context.Planets.Where(p => p.UniverseId == uds.UniverseId).Select(p => p.Id).ToList()
        });

        if (cleanup)
        {
            creation.DeleteUniverse(uds.UniverseId);

            Assert.IsTrue(context.Universes.Count(u => u.Id == uds.UniverseId) == 0);
            Assert.IsTrue(context.Zones.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsTrue(context.Stars.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsTrue(context.Planets.Count(s => s.UniverseId == uds.UniverseId) == 0);
        }
    }

    /// <summary>
    /// Test the creation of Stars and all the values they can have
    /// </summary>
    /// <param name="universeName"></param>
    /// <param name="cleanup"></param>
    [TestMethod, TestCategory("DatabaseTest")]
    // [DataRow("Test Star Class Creation", false)] // Used for testing
    [DataRow("Test Star Class Creation", true)]
    public void TestStarClassCreation(String universeName, Boolean cleanup)
    {
        using var context = new UniverseContext();
        Creation creation = new Creation();

        UniverseDefaultSettings uds = new UniverseDefaultSettings
        {
            Name = universeName,
            UniverseId = new Universe().Id
        };

        creation.CreateUniverse(universeDefaultSettings: uds);
        creation.CreateZones(universeDefaultSettings: uds);

        creation.CreateStars(new StarDefaultSettings
        {
            UniverseId = uds.UniverseId,
            StarCount = 1,
            StarClass = Star.StarClassEnum.A,
            StarColor = Star.StarColorEnum.Blue,
            Name = "Starry McStarface"
        });
        creation.CreateStars(new StarDefaultSettings
        {
            UniverseId = uds.UniverseId,
            StarCount = 1,
            StarClass = Star.StarClassEnum.B,
            StarColor = Star.StarColorEnum.BlueWhite
        });
        creation.CreateStars(new StarDefaultSettings
        {
            UniverseId = uds.UniverseId,
            StarCount = 1,
            StarClass = Star.StarClassEnum.F,
            StarColor = Star.StarColorEnum.White
        });
        creation.CreateStars(new StarDefaultSettings
        {
            UniverseId = uds.UniverseId,
            StarCount = 1,
            StarClass = Star.StarClassEnum.G,
            StarColor = Star.StarColorEnum.YellowWhite
        });
        creation.CreateStars(new StarDefaultSettings
        {
            UniverseId = uds.UniverseId,
            StarCount = 1,
            StarClass = Star.StarClassEnum.M,
            StarColor = Star.StarColorEnum.Yellow
        });
        creation.CreateStars(new StarDefaultSettings
        {
            UniverseId = uds.UniverseId,
            StarCount = 1,
            StarClass = Star.StarClassEnum.K,
            StarColor = Star.StarColorEnum.LightOrange
        });
        creation.CreateStars(new StarDefaultSettings
        {
            UniverseId = uds.UniverseId,
            StarCount = 1,
            StarClass = Star.StarClassEnum.O,
            StarColor = Star.StarColorEnum.OrangeRed
        });


        List<Star> stars = context.Stars.Where(s => s.UniverseId == uds.UniverseId).ToList();

        Assert.IsTrue(context.Universes.Count(u => u.Id == uds.UniverseId) > 0);
        Assert.IsTrue(context.Zones.Count(z => z.UniverseId == uds.UniverseId) > 0);
        Assert.IsTrue(stars.Count == 7);

        Assert.IsTrue(stars.First().Name == "Starry McStarface");

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

        Assert.IsTrue(stars.First().UniverseId == uds.UniverseId);

        var starMapPath = creation.CreateStarMap(uds.UniverseId);

        Assert.IsTrue(File.Exists(starMapPath));

        if (cleanup)
        {
            creation.DeleteUniverse(uds.UniverseId);

            Assert.IsTrue(context.Universes.Count(u => u.Id == uds.UniverseId) == 0);
            Assert.IsTrue(context.Zones.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsTrue(context.Stars.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsTrue(context.Planets.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsFalse(File.Exists(starMapPath));
        }
    }

    /// <summary>
    /// Test the creation of Planets and all the values they can have
    /// </summary>
    /// <param name="universeName"></param>
    /// <param name="cleanup"></param>
    [TestMethod, TestCategory("DatabaseTest")]
    // [DataRow("Test Planet Class Creation", false)] // Used for testing
    [DataRow("Test Planet Class Creation", true)]
    public void TestPlanetClassCreation(String universeName, Boolean cleanup)
    {
        using var context = new UniverseContext();
        Creation creation = new Creation();

        UniverseDefaultSettings uds = new UniverseDefaultSettings
        {
            Name = universeName,
            UniverseId = new Universe().Id,
        };

        creation.CreateUniverse(universeDefaultSettings: uds);
        creation.CreateZones(universeDefaultSettings: uds);
        creation.CreateStars(
            starDefaultSettings: new StarDefaultSettings(createPlanets: true, universeId: uds.UniverseId));
        creation.CreatePlanets(planetDefaultSettings: new PlanetDefaultSettings
        {
            UniverseId = uds.UniverseId,
            Name = "Planet McPlanetface",
            Population = 1234,
            Count = 1,
            StarList = new List<Star> { context.Stars.First(s => s.UniverseId == uds.UniverseId), }
        });
        creation.CreatePlanets(planetDefaultSettings: new PlanetDefaultSettings
        {
            UniverseId = uds.UniverseId,
            Count = 2,
            StarList = new List<Star> { context.Stars.First(s => s.UniverseId == uds.UniverseId), }
        });

        Assert.ThrowsException<Exception>(() =>
            creation.CreatePlanets(planetDefaultSettings: new PlanetDefaultSettings
            {
                UniverseId = uds.UniverseId,
                Name = "Planet McPlanetface",
                Count = 1,
                StarList = new List<Star> { context.Stars.First(s => s.UniverseId == uds.UniverseId), }
            }));

        Assert.ThrowsException<Exception>(() => creation.CreatePlanets(planetDefaultSettings: new PlanetDefaultSettings
        {
            UniverseId = uds.UniverseId,
            Name = "Planet McPlanetface 2",
            StarList = context.Stars.Where(s => s.UniverseId == uds.UniverseId).ToList()
        }));

        Assert.IsTrue(context.Universes.Count(u => u.Id == uds.UniverseId) > 0);
        Assert.IsTrue(context.Zones.Count(s => s.UniverseId == uds.UniverseId) > 0);
        Assert.IsTrue(context.Stars.Count(s => s.UniverseId == uds.UniverseId) > 0);
        Assert.IsTrue(context.Planets.Count(s => s.UniverseId == uds.UniverseId) > 0);
        Assert.IsTrue(context.Planets.First(s => s.UniverseId == uds.UniverseId).UniverseId == uds.UniverseId);
        Assert.IsTrue(
            context.Planets.Count(p => p.UniverseId == uds.UniverseId && p.Name == "Planet McPlanetface") == 1);
        Assert.IsTrue(context.Planets.First(p => p.Name == "Planet McPlanetface").Population == 1234);

        var starMapPath = creation.CreateStarMap(uds.UniverseId);

        Assert.IsTrue(File.Exists(starMapPath));

        if (cleanup)
        {
            creation.DeleteUniverse(uds.UniverseId);

            Assert.IsTrue(context.Universes.Count(u => u.Id == uds.UniverseId) == 0);
            Assert.IsTrue(context.Zones.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsTrue(context.Stars.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsTrue(context.Planets.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsFalse(File.Exists(starMapPath));
        }
    }
    
    /// <summary>
    /// Test the creation of Cities and all the values they can have
    /// </summary>
    /// <param name="universeName"></param>
    /// <param name="cleanup"></param>
    [TestMethod, TestCategory("DatabaseTest")]
    // [DataRow("Test Planet Class Creation", false)] // Used for testing
    [DataRow("Test City Creation", true)]
    public void TestCityCreation(String universeName, Boolean cleanup)
    {
        using var context = new UniverseContext();
        Creation creation = new Creation();
        
        UniverseDefaultSettings uds = new UniverseDefaultSettings
        {
            Name = universeName,
            UniverseId = new Universe().Id,
        };
        
        creation.CreateUniverse(universeDefaultSettings: uds);
        creation.CreateZones(universeDefaultSettings: uds);
        
        
        creation.CreateStars(starDefaultSettings: new StarDefaultSettings
        {
            UniverseId = uds.UniverseId,
            StarCount = 1
        });
        
        creation.CreatePlanets(planetDefaultSettings: new PlanetDefaultSettings
        {
            UniverseId = uds.UniverseId,
            Count = 2,
        });
        
        Assert.ThrowsException<Exception>(() => creation.CreateCities(cityDefaultSettings: new CityDefaultSettings
        {
            UniverseId = uds.UniverseId,
            Name = "City McCityface",
            PlanetList =  context.Planets.Where(s => s.UniverseId == uds.UniverseId).ToList()
        }));
        
        Assert.ThrowsException<Exception>(() => creation.CreateCities(cityDefaultSettings: new CityDefaultSettings
        {
            UniverseId = uds.UniverseId,
            Population = 1,
            PlanetList = null
        }));

        creation.CreateCities(new CityDefaultSettings
        {
            UniverseId = uds.UniverseId,
            Name = "City McCityface 2",
            Count = 1,
            PlanetList = new List<Planet>{context.Planets.First(p => p.UniverseId == uds.UniverseId),}
        });
        Assert.IsTrue(context.Cities.Count(c => c.Id == uds.UniverseId && c.Name == "City McCityface 2") == 1);
        
        Assert.ThrowsException<Exception>(() => creation.CreateCities(new CityDefaultSettings
        {
            UniverseId = uds.UniverseId,
            Name = "City McCityface 2",
            Count = 1,
            PlanetList = new List<Planet>{context.Planets.First(p => p.UniverseId == uds.UniverseId),}
        }));

        creation.CreateCities(new CityDefaultSettings
        {
            UniverseId = uds.UniverseId,
            Name = "City McCityface 3",
            Population = 1000,
        });
        Assert.IsTrue(context.Cities.First(c => c.UniverseId == uds.UniverseId && c.Name == "City McCityface 3").Population == 1000);
        
        if (cleanup)
        {
            creation.DeleteUniverse(uds.UniverseId);

            Assert.IsTrue(context.Universes.Count(u => u.Id == uds.UniverseId) == 0);
            Assert.IsTrue(context.Zones.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsTrue(context.Stars.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsTrue(context.Planets.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsTrue(context.Cities.Count(s => s.UniverseId == uds.UniverseId) == 0);
        }
    }

    /// <summary>
    /// Test the creation of characters and all the values a Character can have
    /// </summary>
    /// <param name="universeName"></param>
    /// <param name="cleanup"></param>
    [TestMethod, TestCategory("DatabaseTest")]
    // [DataRow("Test Character Creation", false)] // Used for testing
    [DataRow("Test Character Creation", true)]
    public void TestCharacterCreation(String universeName, Boolean cleanup)
    {
        using var context = new UniverseContext();
        Creation creation = new Creation();

        UniverseDefaultSettings uds = new UniverseDefaultSettings
        {
            Name = universeName,
            UniverseId = new Universe().Id,
        };

        // Create the base universe
        creation.CreateUniverse(universeDefaultSettings: uds);
        creation.CreateZones(universeDefaultSettings: uds);

        // Create some stars and planets for funsies
        creation.CreateStars(starDefaultSettings: new StarDefaultSettings
        {
            UniverseId = uds.UniverseId,
            CreatePlanets = true
        });

        // John Doe
        creation.CreateCharacter(new CharacterDefaultSettings
        {
            UniverseId = uds.UniverseId,
            Count = 1,
            Balanced = (false, 0),
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
            CurrentPlanetId = context.Planets.First(p => p.UniverseId == uds.UniverseId).Id,
            CrimeChance = new[] { 50, 50 },
            InitialReaction = "Warm"
        });

        // Jane Doe
        creation.CreateCharacter(new CharacterDefaultSettings
        {
            UniverseId = uds.UniverseId,
            Count = 1,
            Balanced = (false, 0),
            Gender = Character.GenderEnum.Female,
            First = "Jane",
            Last = "Doe",
        });

        // 13 randos
        creation.CreateCharacter(new CharacterDefaultSettings
        {
            UniverseId = uds.UniverseId,
            Balanced = (false, 0),
            Count = 13
        });

        Assert.IsTrue(context.Universes.Count(u => u.Id == uds.UniverseId) > 0);
        Assert.IsTrue(context.Zones.Count(s => s.UniverseId == uds.UniverseId) > 0);
        Assert.IsTrue(context.Stars.Count(s => s.UniverseId == uds.UniverseId) > 0);
        Assert.IsTrue(context.Planets.Count(s => s.UniverseId == uds.UniverseId) > 0);

        List<Character> chars = context.Characters.Where(c => c.UniverseId == uds.UniverseId).ToList();

        Assert.IsTrue(context.Characters.Count(c => c.UniverseId == uds.UniverseId) == 15);
        Assert.IsTrue(chars.Count == 15);

        Character john = chars[0];
        Character jane = chars[1];

        Assert.IsTrue(john.First == "John");
        Assert.IsTrue(john.Last == "Doe");
        Assert.IsTrue(john.Name == "John Doe");
        Assert.IsTrue(john.Age == 25);
        Assert.IsTrue(john.HairStyle == "Long Straight");
        Assert.IsTrue(john.HairCol == "Brown");
        Assert.IsTrue(john.EyeCol == "Blue");
        Assert.IsTrue(john.Title == "Dude");
        Assert.IsTrue(john.SkinCol == "Pale");
        Assert.IsTrue(john.Height == 120);
        Assert.IsTrue(john.Gender == Character.GenderEnum.Male);
        Assert.IsTrue(john.CrimeChance == 50);
        Assert.IsTrue(john.InitialReaction == "Warm");
        Assert.IsTrue(john.CurrentLocationId == context.Planets.First(p => p.UniverseId == uds.UniverseId).Id);

        Assert.IsTrue(jane.First == "Jane");
        Assert.IsTrue(jane.Last == "Doe");
        Assert.IsTrue(jane.Gender == Character.GenderEnum.Female);

        Assert.IsTrue(john.UniverseId == uds.UniverseId);

        if (cleanup)
        {
            creation.DeleteUniverse(uds.UniverseId);

            Assert.IsTrue(context.Universes.Count(u => u.Id == uds.UniverseId) == 0);
            Assert.IsTrue(context.Zones.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsTrue(context.Stars.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsTrue(context.Planets.Count(s => s.UniverseId == uds.UniverseId) == 0);
        }
    }

    /// <summary>
    /// Test the creation of ships and all the values a Ship can have
    /// </summary>
    /// <param name="universeName"></param>
    /// <param name="cleanup"></param>
    [TestMethod, TestCategory("DatabaseTest")]
    // [DataRow("Test Ship Creation", false)] // Used for testing
    [DataRow("Test Ship Creation", true)]
    public void TestShipCreation(String universeName, Boolean cleanup)
    {
        using var context = new UniverseContext();
        Creation creation = new Creation();
        UniverseDefaultSettings uds = new UniverseDefaultSettings
        {
            UniverseId = new Universe().Id,
            Name = universeName,
        };

        creation.CreateUniverse(universeDefaultSettings: uds);
        creation.CreateZones(universeDefaultSettings: uds);
        creation.CreateShips(new ShipDefaultSettings
        {
            UniverseId = uds.UniverseId,
            Count = 100,
            CreateCrew = false,
            HomeId = uds.UniverseId,
            LocationId = uds.UniverseId
        });
        creation.CreateShips(new ShipDefaultSettings
        {
            UniverseId = uds.UniverseId,
            Count = 1,
            CreateCrew = false,
            HomeId = uds.UniverseId,
            LocationId = uds.UniverseId,
            HullType = Hull.HullTypeEnum.Carrier
        });

        Assert.IsTrue(context.Universes.Count(u => u.Id == uds.UniverseId) > 0);
        Assert.IsTrue(context.Ships.Count(s => s.UniverseId == uds.UniverseId) >= 10);

        if (cleanup)
        {
            creation.DeleteUniverse(uds.UniverseId);

            Assert.IsTrue(context.Universes.Count(u => u.Id == uds.UniverseId) == 0);
            Assert.IsTrue(context.Zones.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsTrue(context.Stars.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsTrue(context.Planets.Count(s => s.UniverseId == uds.UniverseId) == 0);
        }
    }

    /// <summary>
    /// Test the creation of the full universe.
    /// WARNING: This may result in incomplete coverage as it will heavily rely on the randomization of generation
    /// as opposed to easily assertable values. This is largely to ensure the database works as intended.
    /// </summary>
    /// <param name="universeName"></param>
    /// <param name="cleanup"></param>
    [TestMethod, TestCategory("DatabaseTest")]
    // [DataRow("My Universe", false)] // Used for testing
    [DataRow("Test Full Creation", true)]
    public void TestFullCreation(String universeName, Boolean cleanup)
    {
        using var context = new UniverseContext();
        Creation creation = new Creation();
        UniverseDefaultSettings uds = new UniverseDefaultSettings
        {
            Name = universeName,
            UniverseId = new Universe().Id,
            StarDefaultSettings = new StarDefaultSettings(createPlanets: false),
            PlanetDefaultSettings = new PlanetDefaultSettings(),
            CharacterDefaultSettings = new CharacterDefaultSettings(count: 100),
            ShipDefaultSettings = new ShipDefaultSettings(count: 10),
            PoiDefaultSettings = new PoiDefaultSettings()
        };

        _ = creation.CreateFullUniverse(universeDefaultSettings: uds);

        Assert.IsTrue(context.Universes.Count(u => u.Id == uds.UniverseId) > 0);
        Assert.IsTrue(context.Universes.Single(u => u.Id == uds.UniverseId).GridX == 8);
        Assert.IsTrue(context.Universes.Single(u => u.Id == uds.UniverseId).GridY == 10);
        Assert.IsTrue(context.Zones.Count(s => s.UniverseId == uds.UniverseId) > 0);
        Assert.IsTrue(context.Stars.Count(s => s.UniverseId == uds.UniverseId) > 0);
        Assert.IsTrue(context.Planets.Count(s => s.UniverseId == uds.UniverseId) > 0);
        Assert.IsTrue(context.Characters.Count(s => s.UniverseId == uds.UniverseId) > 0);
        Assert.IsTrue(context.Ships.Count(s => s.UniverseId == uds.UniverseId) > 0);

        var starMapPath = creation.CreateStarMap(uds.UniverseId);

        Assert.IsTrue(File.Exists(starMapPath));

        if (cleanup)
        {
            creation.DeleteUniverse(uds.UniverseId);

            Assert.IsTrue(context.Universes.Count(u => u.Id == uds.UniverseId) == 0);
            Assert.IsTrue(context.Zones.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsTrue(context.Stars.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsTrue(context.Planets.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsTrue(context.Characters.Count(c => c.UniverseId == uds.UniverseId) == 0);
            Assert.IsTrue(context.Ships.Count(s => s.UniverseId == uds.UniverseId) == 0);
            Assert.IsFalse(File.Exists(starMapPath));
        }
    }
}