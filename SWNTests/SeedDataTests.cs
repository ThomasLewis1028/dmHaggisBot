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
        List<ShipHullObject> hulls = new DbInitializer().GetShipHullData();
        Assert.IsTrue(hulls.Count > 0);
    }
    
    [TestMethod, TestCategory("SeedData")]
    public void TestShipFitingSeedData()
    {
        List<ShipFittingObject> hulls = new DbInitializer().GetShipFittingData();
        Assert.IsTrue(hulls.Count > 0);
    }
    
    [TestMethod, TestCategory("SeedData")]
    public void TestGetShipDefenseData()
    {
        List<ShipDefenseObject> hulls = new DbInitializer().GetShipDefenseData();
        Assert.IsTrue(hulls.Count > 0);
    }
    
    [TestMethod, TestCategory("SeedData")]
    public void TestGetShipWeaponData()
    {
        List<ShipWeaponObject> hulls = new DbInitializer().GetShipWeaponData();
        Assert.IsTrue(hulls.Count > 0);
    }
    
    [TestMethod, TestCategory("SeedData")]
    public void TestGetNamingData()
    {
        List<Naming> hulls = new DbInitializer().GetNamingData();
        Assert.IsTrue(hulls.Count > 0);
    }
}