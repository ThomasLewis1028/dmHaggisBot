using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWNUniverseGenerator.Models;
using SWNUniverseGenerator.Migrations;

namespace SWNTests;

[TestClass]
public class SeedDataTests
{

    [TestMethod, TestCategory("SeedData")]
    public void TestShipHullSeedData()
    {
        List<ShipHull> hulls = new DbInitializer().GetShipHullData();
        Assert.IsTrue(hulls.Count > 0);
    }
    
    [TestMethod, TestCategory("SeedData")]
    public void TestShipFitingSeedData()
    {
        List<ShipFitting> hulls = new DbInitializer().GetShipFittingData();
        Assert.IsTrue(hulls.Count > 0);
    }
    
    [TestMethod, TestCategory("SeedData")]
    public void TestGetShipDefenseData()
    {
        List<ShipDefense> hulls = new DbInitializer().GetShipDefenseData();
        Assert.IsTrue(hulls.Count > 0);
    }
    
    [TestMethod, TestCategory("SeedData")]
    public void TestGetShipWeaponData()
    {
        List<ShipWeapon> hulls = new DbInitializer().GetShipWeaponData();
        Assert.IsTrue(hulls.Count > 0);
    }
}