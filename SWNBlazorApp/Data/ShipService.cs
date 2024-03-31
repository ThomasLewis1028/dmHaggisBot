using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class ShipService : DataService<ShipService>
{
    public ShipService(UniverseContext context, ILogger<ShipService> logger) : base(context, logger)
    {

    }
    
    public Task<Ship> GetShipAsync(string shipId)
    {
        Ship result;
        var repo = new Repository<Ship>(_context);
        result = repo.GetById(shipId);
        
        return Task.FromResult(result);
    }
    
    public Task<List<Ship>> GetShipsAsync(string universeId)
    {
        List<Ship> result;
        var repo = new Repository<Ship>(_context);
        var entityList = repo.Search(c => c.UniverseId == universeId).ToList();
        result = entityList.Cast<Ship>().ToList();
    
        return Task.FromResult(result);
    }

    public Task<Hull> GetShipHull(string hullId)
    {
        var repo = new Repository<Hull>(Context);
        var result = repo.GetById(hullId);

        return Task.FromResult(result);
    }
}