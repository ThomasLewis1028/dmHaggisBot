using System.Reflection;
using MudBlazor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWNUniverseGenerator;
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
        var pers = JObject.Parse(File.ReadAllText(dataPath + "/Data/persistence.json"));
        Persistence persistence = JsonConvert.DeserializeObject<Persistence>(pers.ToString());

        if (persistence.CurrentUniverseName == null)
            return Task.FromResult<Universe>(null);

        Universe universe = new Creation().LoadUniverse(persistence.CurrentUniverseName);
        
        
        return Task.FromResult(universe);
    }

    public Task<List<Creation.UniverseInfo>> GetUniverseListAsync()
    {
        return Task.FromResult(new Creation().GetUniverseList());
    }

    public Task<bool> DeleteUniverseAsync(string universeName)
    {
        new Creation().DeleteUniverse(universeName);
        
        if(File.Exists("wwwroot/images/starmaps/" + universeName + ".png"))
            File.Delete("wwwroot/images/starmaps/" + universeName + ".png");
        
        return Task.FromResult(true);
    }

    public Task<bool> IsUniverseLoaded()
    {
        var pers = JObject.Parse(File.ReadAllText(dataPath + "/Data/persistence.json"));
        Persistence persistence = JsonConvert.DeserializeObject<Persistence>(pers.ToString());

        if (persistence.CurrentUniverseName == null)
            return Task.FromResult(false);

        return Task.FromResult(true);
    }
}

