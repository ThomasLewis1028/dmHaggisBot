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
}