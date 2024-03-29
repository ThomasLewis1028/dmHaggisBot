using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class ZoneService : DataService
{
    public ZoneService(UniverseContext context) : base(context)
    {

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

    public Task<int> GetPlanetCount(string zoneId)
    {
        int result;
        using (var repo = new Repository<Planet>(Context))
        {
            result = repo.Count(c => c.ZoneId == zoneId);
        }
    
        return Task.FromResult(result);
    } 
}