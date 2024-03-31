using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class ZoneService : DataService<ZoneService>
{
    public ZoneService(UniverseContext context, ILogger<ZoneService> logger) : base(context, logger)
    {

    }
    
    public Task<List<Zone>> GetZonesAsync(string universeId)
    {
        List<Zone> result;
        var repo = new Repository<Zone>(_context);
        var entityList = repo.Search(c => c.UniverseId == universeId).ToList();
        result = entityList.Cast<Zone>().ToList();

        return Task.FromResult(result);
    }

    public Task<int> GetPlanetCount(string zoneId)
    {
        int result;
        var repo = new Repository<Planet>(_context);
        result = repo.Count(c => c.ZoneId == zoneId);
    
        return Task.FromResult(result);
    } 
}