using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class UniverseService
{
    private String dataPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    
    // public Task<Boolean> SetUniverse(Universe universe)
    // {
    //     _universe = universe;
    //     return Task.FromResult(true);
    // }
    
    public Task<Universe> GetUniverseAsync()
    {
        var pers = JObject.Parse(File.ReadAllText(dataPath + "/persistence.json"));
        Persistence persistence = JsonConvert.DeserializeObject<Persistence>(pers.ToString());

        if (persistence.CurrentUniverseName == null)
            return null;

        var univ = JObject.Parse(File.ReadAllText(dataPath + "/UniverseFiles/" + persistence.CurrentUniverseName + ".json"));
        Universe universe = JsonConvert.DeserializeObject<Universe>(univ.ToString());
        
        return Task.FromResult(universe);
    }

    public Task<List<UniverseRow>> GetUniverseListAsync()
    {
        List<UniverseRow> universeRows = new();

        if (Directory.Exists(dataPath + "/UniverseFiles/"))
        {
            string[] fileEntries = Directory.GetFiles(dataPath + "/UniverseFiles/");
            foreach(string fileName in fileEntries)
            {
                var univ = JObject.Parse(File.ReadAllText(fileName));
                Universe universe = JsonConvert.DeserializeObject<Universe>(univ.ToString());
                UniverseRow universeRow = new UniverseRow()
                {
                    Name = universe.Name,
                    GridX = universe.Grid.X,
                    GridY = universe.Grid.Y,
                    StarCount = universe.Stars.Count,
                    PlanetCount = universe.Planets.Count,
                    ShipCount = universe.Ships.Count,
                    CharCount = universe.Characters.Count
                };
                universeRows.Add(universeRow);
            }
        }

        return Task.FromResult(universeRows);
    }

    public Task<bool> DeleteUniverseAsync(string universeName)
    {
        if (File.Exists(dataPath + "/UniverseFiles/" + universeName + ".json"))
        {
            File.Delete(dataPath + "/UniverseFiles/" + universeName + ".json");
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }
    
    public class UniverseRow
     {
         public String Name { get; set; }
     
         public int GridX;
         public int GridY;
     
         public int StarCount;
         public int PlanetCount;
         public int ShipCount;
         public int CharCount;
     }
}

