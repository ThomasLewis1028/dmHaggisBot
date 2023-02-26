using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWNUniverseGenerator;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNTests;

[TestClass]
public class DatabaseTests
{
    [TestMethod]
    public void TestCreate()
    {
        Universe universe = new Universe()
        {
            Name = "TestCreateUniverse",
            GridX = 8,
            GridY = 10
        };

        UniverseContext uc = new UniverseContext();
        uc.Universes.Add(universe);

        uc.SaveChanges();

        Universe ucGet = uc.Universes.Where(u => u.Name == "TestCreateUniverse").FirstOrDefault();
        
        Assert.AreEqual(ucGet.Name, universe.Name);
    }
}