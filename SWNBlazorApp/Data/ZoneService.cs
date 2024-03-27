using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class ZoneService
{
    private readonly IDbContextFactory<UniverseContext> _contextFactory;

    public ZoneService(IDbContextFactory<UniverseContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public Task<List<Zone>> GetZonesAsync(string universeId)
    {
        List<Zone> result;
        using (var context = _contextFactory.CreateDbContext())
        {
            using (var repo = new Repository<Zone>(context))
            {
                var entityList = repo.Search(c => c.UniverseId == universeId).ToList();
                result = entityList.Cast<Zone>().ToList();
            }
        }
        return Task.FromResult(result);
    }

    public Task<int> GetPlanetCount(string zoneId)
    {
        int result;
        using (var context = _contextFactory.CreateDbContext())
        {
            using (var repo = new Repository<Planet>(context))
            {
                result = repo.Count(c => c.ZoneId == zoneId);
            }
        }
        return Task.FromResult(result);
    } 
}