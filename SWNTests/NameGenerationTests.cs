using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWNUniverseGenerator;
using SWNUniverseGenerator.Database;
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
    public void TestGenerateChain()
    {
        using (var context = new UniverseContext())
        {
            using (var nameRepo = new Repository<Naming>(context))
            {
                var entityList = nameRepo.Search(n => n.NameType == "MaleName").ToList();
                var maleList = entityList.Cast<Naming>().ToList();
                NameGeneration nameGeneration = new NameGeneration();
                var actual = nameGeneration.GenerateChain(maleList);
                
                Assert.AreEqual(true, actual);                
            }
        }
    }
    
    [TestMethod, TestCategory("UnitTest")]
    public void TestGenerateName()
    {
        using (var context = new UniverseContext())
        {
            using (var nameRepo = new Repository<Naming>(context))
            {
                var entityList = nameRepo.Search(n => n.NameType == "MaleName").ToList();
                var maleList = entityList.Cast<Naming>().ToList();
                NameGeneration nameGeneration = new NameGeneration();
                var result = nameGeneration.GenerateChain(maleList);
                Assert.AreEqual(true, result); 

                var actual = nameGeneration.GenerateName();  
                Assert.IsTrue(Regex.IsMatch(actual, "\\w"));
            }
        }
     
    }
    
    [TestMethod, TestCategory("UnitTest")]
    [DataRow ("C", "^(C)[a-zA-Z]*$")]
    [DataRow ("M", "^(M)[a-zA-Z]*$")]
    [DataRow ("A", "^(A)[a-zA-Z]*$")]
    [DataRow ("Z", "^(Z)[a-zA-Z]*$")]
    public void TestGenerateNameFirstCharSame(string search, string expected)
    {
        using (var context = new UniverseContext())
        {
            using (var nameRepo = new Repository<Naming>(context))
            {
                var entityList = nameRepo.Search(n => n.NameType == "MaleName" && n.Name.StartsWith(search)).ToList();
                var maleList = entityList.Cast<Naming>().ToList();
                NameGeneration nameGeneration = new NameGeneration();
                var result = nameGeneration.GenerateChain(maleList);
                Assert.AreEqual(true, result); 

                var actual = nameGeneration.GenerateName();
                Assert.IsTrue(Regex.IsMatch(actual, expected));
            }
        }
     
    }
}