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
        List<Hull> hulls = new DbInitializer().GetShipHullData();
        Assert.IsTrue(hulls.Count > 0);
    }
    
    [TestMethod, TestCategory("SeedData")]
    public void TestShipFitingSeedData()
    {
        List<Fitting> hulls = new DbInitializer().GetShipFittingData();
        Assert.IsTrue(hulls.Count > 0);
    }
    
    [TestMethod, TestCategory("SeedData")]
    public void TestGetShipDefenseData()
    {
        List<Defense> hulls = new DbInitializer().GetShipDefenseData();
        Assert.IsTrue(hulls.Count > 0);
    }
    
    [TestMethod, TestCategory("SeedData")]
    public void TestGetShipWeaponData()
    {
        List<Armament> hulls = new DbInitializer().GetShipArmamentData();
        Assert.IsTrue(hulls.Count > 0);
    }
    
    [TestMethod, TestCategory("SeedData")]
    public void TestGetNamingData()
    {
        List<Naming> hulls = new DbInitializer().GetNamingData();
        Assert.IsTrue(hulls.Count > 0);
    }

    [TestMethod, TestCategory("SeedData")]
    public void TestGetSpecHullData()
    {
        var dbInit = new DbInitializer();

        var shipSpec = dbInit.Deserialize<ShipSpec>("ShipSpec.json");
        
        var hullData = dbInit.GetShipHullData();
        List<Spec> specData = dbInit.GetSpecData(hullData, shipSpec);
        Assert.IsTrue(specData.Count > 0);
    }
    
    [TestMethod, TestCategory("SeedData")]
    public void TestGetSpecArmamentData()
    {
        var dbInit = new DbInitializer();

        var shipSpec = dbInit.Deserialize<ShipSpec>("ShipSpec.json");
        var hullData = dbInit.GetShipHullData();
        var specData = dbInit.GetSpecData(hullData, shipSpec);
        
        var armamentData = dbInit.GetShipArmamentData();
        
        List<SpecArmament> specArmamentData = dbInit.GetSpecArmamentData(specData, armamentData, shipSpec);
        Assert.IsTrue(specData.Count > 0);
    }
    
    
    [TestMethod, TestCategory("SeedData")]
    public void TestGetSpecDefenseData()
    {
        var dbInit = new DbInitializer();

        var shipSpec = dbInit.Deserialize<ShipSpec>("ShipSpec.json");
        var hullData = dbInit.GetShipHullData();
        var specData = dbInit.GetSpecData(hullData, shipSpec);
        
        var defenseData = dbInit.GetShipDefenseData();
        
        List<SpecDefense> specDefenseDataData = dbInit.GetSpecDefenseData(specData, defenseData, shipSpec);
        Assert.IsTrue(specData.Count > 0);
    }
    
        
    [TestMethod, TestCategory("SeedData")]
    public void TestGetSpecFittingData()
    {
        var dbInit = new DbInitializer();

        var shipSpec = dbInit.Deserialize<ShipSpec>("ShipSpec.json");
        var hullData = dbInit.GetShipHullData();
        var specData = dbInit.GetSpecData(hullData, shipSpec);
        
        var fittingData = dbInit.GetShipFittingData();
        
        List<SpecFitting> specFittingData = dbInit.GetSpecFittingData(specData, fittingData, shipSpec);
        Assert.IsTrue(specData.Count > 0);
    }
}