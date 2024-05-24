using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class PlanetService : DataService<PlanetService>
{
    public PlanetService(UniverseContext context, ILogger<PlanetService> logger) : base(context, logger)
    {

    }
    
    public Task<Planet> GetPlanetAsync(string planetId)
    {
        Planet result;
        var repo = new Repository<Planet>(_context);
        result = repo.GetById(planetId);

        
        return Task.FromResult(result);
    }
    
    public Task<List<Planet>> GetPlanetsAsync(string universeId)
    {
        List<Planet> result;
        var repo = new Repository<Planet>(_context);
        var entityList = repo.Search(c => c.UniverseId == universeId).ToList();
        result = entityList.Cast<Planet>().ToList();
        
        return Task.FromResult(result);
    }

    public Task<TechLevel> GetPlanetTechLevel(string techLevelId)
    {
        TechLevel result;
        var repo = new Repository<TechLevel>(_context);
        result = repo.GetById(techLevelId);
        
        return Task.FromResult(result);
    }

    public Task<List<Planet>> GetPlanetsByZoneAsync(string zoneId)
    {
        List<Planet> result;
        var repo = new Repository<Planet>(_context);
        var entityList = repo.Search(c => c.ZoneId == zoneId).ToList();
        result = entityList.Cast<Planet>().ToList();
        
        return Task.FromResult(result);
    }

    public Task<int> GetPOICountAsync(string planetID)
    {
        int result;
        var repo = new Repository<PointOfInterest>(_context);
        result = repo.Count(c => c.LocationId == planetID);
    
        return Task.FromResult(result);
    }
    
    public Task<List<PointOfInterest>> GetPOIsByStarAsync(string planetID)
    {
        List<PointOfInterest> result;
        var repo = new Repository<PointOfInterest>(_context);
        var entityList = repo.Search(c => c.LocationId == planetID).ToList();
        result = entityList.Cast<PointOfInterest>().ToList();
        
        return Task.FromResult(result);
    }
}