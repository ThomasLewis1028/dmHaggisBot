using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWNUniverseGenerator.DeserializedObjects;
using SWNUniverseGenerator.Migrations;

namespace SWNTests;

[TestClass]
public class SeedDataTests
{

    [TestMethod, TestCategory("SeedData")]
    public void TestShipHullSeedData()
    {
        List<ShipHull> hulls = SeedData.GetShipHullData();
        Assert.IsTrue(hulls.Count > 0);
    }
    
    [TestMethod, TestCategory("SeedData")]
    public void TestShipFitingSeedData()
    {
        List<ShipFitting> hulls = SeedData.GetShipFittingData();
        Assert.IsTrue(hulls.Count > 0);
    }
    
    [TestMethod, TestCategory("SeedData")]
    public void TestGetShipDefenseData()
    {
        List<ShipDefense> hulls = SeedData.GetShipDefenseData();
        Assert.IsTrue(hulls.Count > 0);
    }
    
    [TestMethod, TestCategory("SeedData")]
    public void TestGetShipWeaponData()
    {
        List<ShipWeapon> hulls = SeedData.GetShipWeaponData();
        Assert.IsTrue(hulls.Count > 0);
    }
}