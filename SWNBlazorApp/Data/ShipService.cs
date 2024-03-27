using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class ShipService
{
    private readonly IDbContextFactory<UniverseContext> _contextFactory;

    public ShipService(IDbContextFactory<UniverseContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public Task<Ship> GetShipsync(string shipId)
    {
        Ship result;
        using (var context = _contextFactory.CreateDbContext())
        {
            using (var repo = new Repository<Ship>(context))
            {
                result = repo.GetById(shipId);
            }
        }
        return Task.FromResult(result);
    }
    
    public Task<List<Ship>> GetZonesAsync(string universeId)
    {
        List<Ship> result;
        using (var context = _contextFactory.CreateDbContext())
        {
            using (var repo = new Repository<Ship>(context))
            {
                var entityList = repo.Search(c => c.UniverseId == universeId).ToList();
                result = entityList.Cast<Ship>().ToList();
            }
        }
        return Task.FromResult(result);
    }
}