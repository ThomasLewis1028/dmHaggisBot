using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class PlanetService : DataService
{
    public PlanetService(UniverseContext context) : base(context)
    {

    }
    
    public Task<Planet> GetPlanetAsync(string planetId)
    {
        Planet result;
        using (var repo = new Repository<Planet>(Context))
        {
            result = repo.GetById(planetId);
        }
        
        return Task.FromResult(result);
    }
    
    public Task<List<Planet>> GetPlanetsAsync(string universeId)
    {
        List<Planet> result;
        using (var repo = new Repository<Planet>(Context))
        {
            var entityList = repo.Search(c => c.UniverseId == universeId).ToList();
            result = entityList.Cast<Planet>().ToList();
        }
        
        return Task.FromResult(result);
    }

    public Task<List<Planet>> GetPlanetsByZoneAsync(string zoneId)
    {
        List<Planet> result;
        using (var repo = new Repository<Planet>(Context))
        {
            var entityList = repo.Search(c => c.ZoneId == zoneId).ToList();
            result = entityList.Cast<Planet>().ToList();
        }
        
        return Task.FromResult(result);
    }

}