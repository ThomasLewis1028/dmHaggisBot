using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWNUniverseGenerator;
using SWNUniverseGenerator.CreationTools;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class UniverseService : DataService
{
    public UniverseService(UniverseContext context) : base(context)
    {

    }
    
    // public Task<Boolean> SetUniverse(Universe universe)
    // {
    //     _universe = universe;
    //     return Task.FromResult(true);
    // }
    
    public Task<Universe> GetUniverseAsync(string universeID)
    {
        Universe result;
        using (var universeRepo = new Repository<Universe>(Context))
        {
            result = universeRepo.GetById(universeID);
        }
        
        return Task.FromResult(result);
    }

    public Task<List<Universe>> GetUniverseListAsync()
    {
        List<Universe> result;
        using (var repo = new Repository<Universe>(Context))
        {
            result = repo.GetAll().ToList();
        }
        
        return Task.FromResult(result);
    }

    public Task<bool> DeleteUniverseAsync(string universeID)
    {
        bool result;
        using (var universeRepo = new Repository<Universe>(Context))
        {
            result = universeRepo.Delete(universeID);
        }
        
        if(File.Exists("wwwroot/images/starmaps/" + universeID + ".png"))
            File.Delete("wwwroot/images/starmaps/" + universeID + ".png");
        
        return Task.FromResult(result);
    }

    public Task<bool> IsUniverseLoaded()
    {
        // var pers = JObject.Parse(File.ReadAllText(dataPath + "/Data/persistence.json"));
        // Persistence persistence = JsonConvert.DeserializeObject<Persistence>(pers.ToString());
        //
        // if (persistence.CurrentUniverseName == null)
        //     return Task.FromResult(false);

        return Task.FromResult(true);
    }
}

