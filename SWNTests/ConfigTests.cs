using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNTests;

[TestClass]
public class ConfigTests
{
    
    [TestMethod, TestCategory("ConfigTests")]
    public void Test_IsConfigured_Positive()
    {

        // test against this configuration
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .Build();

        // the extension method to test
        Assert.IsNotNull(config);
        Assert.IsNotNull(config.GetConnectionString("DefaultConnection"));
        Assert.IsTrue(config.GetConnectionString("DefaultConnection").Contains("universe.db"));
    }
    
}