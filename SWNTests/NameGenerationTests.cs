using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWNUniverseGenerator;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNTests;

[TestClass]
public class NameGenerationTests
{
    
    
    private IConfiguration InitConfiguration()
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .Build();
        return config;
    }

    
    [TestMethod, TestCategory("UnitTest")]
    public void TestGenerateChain()
    {
        var _context = new UniverseContext(InitConfiguration());
        using (var nameRepo = new Repository<Naming>(_context))
        {
            var entityList = nameRepo.Search(n => n.NameType == "MaleName").ToList();
            var maleList = entityList.Cast<Naming>().ToList();
            NameGeneration nameGeneration = new NameGeneration();
            var actual = nameGeneration.GenerateChain(maleList);
            
            Assert.AreEqual(true, actual);                
        }
    }
    
    [TestMethod, TestCategory("UnitTest")]
    public void TestGenerateName()
    {
        var _context = new UniverseContext(InitConfiguration());
        using (var nameRepo = new Repository<Naming>(_context))
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
    
    [TestMethod, TestCategory("UnitTest")]
    [DataRow ("C", "^(C)[a-zA-Z]*$")]
    [DataRow ("M", "^(M)[a-zA-Z]*$")]
    [DataRow ("A", "^(A)[a-zA-Z]*$")]
    [DataRow ("Z", "^(Z)[a-zA-Z]*$")]
    public void TestGenerateNameFirstCharSame(string search, string expected)
    {
        var _context = new UniverseContext(InitConfiguration());
        using (var nameRepo = new Repository<Naming>(_context))
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