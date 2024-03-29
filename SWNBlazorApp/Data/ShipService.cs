using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class ShipService : DataService
{
    public ShipService(UniverseContext context) : base(context)
    {

    }
    
    public Task<Ship> GetShipsync(string shipId)
    {
        Ship result;
        var repo = new Repository<Ship>(Context);
        result = repo.GetById(shipId);
        
        return Task.FromResult(result);
    }
    
    public Task<List<Ship>> GetZonesAsync(string universeId)
    {
        List<Ship> result;
        var repo = new Repository<Ship>(Context);
        var entityList = repo.Search(c => c.UniverseId == universeId).ToList();
        result = entityList.Cast<Ship>().ToList();
    
        return Task.FromResult(result);
    }
}