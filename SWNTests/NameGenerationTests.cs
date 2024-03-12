using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWNUniverseGenerator;
using SWNUniverseGenerator.CreationTools;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

namespace SWNTests;

[TestClass]
public class NameGenerationTests
{
    
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
    
    [TestMethod, TestCategory("UnitTest")]
    [DataRow(true, "Test1")]
    [DataRow(true, "Test1", "Test2", "Test3")]
    [DataRow(true, "Test1", "Test2", "Test3", "Test4", "Test5")]
    public void TestGenerateChain(bool expected,params String[] nameList)
    {
        NameGeneration nameGeneration = new NameGeneration();
        var actual = nameGeneration.GenerateChain(nameList.ToList());
        Assert.AreEqual(expected, actual);
    }
    
    [TestMethod, TestCategory("UnitTest")]
    [DataRow("(Test)[0-9]", "Test1")]
    [DataRow("(Test)[0-9]", "Test1", "Test2", "Test3")]
    [DataRow("(Test)[0-9]", "Test1", "Test2", "Test3", "Test4", "Test5")]
    public void TestGenerateName(string expected,params String[] nameList)
    {
        NameGeneration nameGeneration = new NameGeneration();
        nameGeneration.GenerateChain(nameList.ToList());
        var actual = nameGeneration.GenerateName();
        Assert.IsTrue(Regex.IsMatch(actual, expected));
    }
}