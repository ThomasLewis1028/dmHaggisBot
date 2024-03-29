using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class StarService : DataService
{
    public StarService(UniverseContext context) : base(context)
    {

    }
    
    public Task<List<Star>> GetStarsAsync(string universeId)
    {
        List<Star> result;
        using (var repo = new Repository<Star>(Context))
        {
            var entityList = repo.Search(c => c.UniverseId == universeId).ToList();
            result = entityList.Cast<Star>().ToList();
        }
        
        return Task.FromResult(result);
    }
    
    public Task<List<Zone>> GetZonesAsync(string universeId)
    {
        List<Zone> result;
        using (var repo = new Repository<Zone>(Context))
        {
            var entityList = repo.Search(c => c.UniverseId == universeId).ToList();
            result = entityList.Cast<Zone>().ToList();
        }
        
        return Task.FromResult(result);
    }

    public Task<bool> CreateCharacterAsync(int count = -1)
    {
        // CreationService creationService = new CreationService();
        // Creation creation = creationService.GetCreationAsync().Result;

        
        return Task.FromResult(true);
    } 
}