using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class StarService : DataService<StarService>
{
    public StarService(UniverseContext context, ILogger<StarService> logger) : base(context, logger)
    {
    }

    public Task<Star> GetStarAsync(string starId)
    {
        var repo = new Repository<Star>(_context);
        var result = repo.GetById(starId);

        return Task.FromResult(result);
    }

    public Task<List<Star>> GetStarsAsync(string universeId)
    {
        List<Star> result;
        var repo = new Repository<Star>(_context);
        var entityList = repo.Search(c => c.UniverseId == universeId).ToList();
        result = entityList.Cast<Star>().ToList();

        return Task.FromResult(result);
    }


    public Task<Star> GetStarByZoneAsync(string zoneId)
    {
        var repo = new Repository<Star>(_context);
        var result = (Star)repo.Search(s => s.ZoneId == zoneId).First();

        return Task.FromResult(result);
    }

    public Task<Zone> GetZoneAsync(string zoneId)
    {
        var repo = new Repository<Zone>(_context);
        var result = repo.GetById(zoneId);

        return Task.FromResult(result);
    }

    public Task<List<Zone>> GetZonesAsync(string universeId)
    {
        List<Zone> result;
        var repo = new Repository<Zone>(_context);
        var entityList = repo.Search(c => c.UniverseId == universeId).ToList();
        result = entityList.Cast<Zone>().ToList();
        
        return Task.FromResult(result);
    }

    public Task<int> GetPOICountAsync(string starID)
    {
        int result;
        var repo = new Repository<PointOfInterest>(_context);
        result = repo.Count(c => c.LocationId == starID);
    
        return Task.FromResult(result);
    }
    
    public Task<List<PointOfInterest>> GetPOIsByStarAsync(string starID)
    {
        List<PointOfInterest> result;
        var repo = new Repository<PointOfInterest>(_context);
        var entityList = repo.Search(c => c.LocationId == starID).ToList();
        result = entityList.Cast<PointOfInterest>().ToList();
        
        return Task.FromResult(result);
    }

    public Task<bool> CreateCharacterAsync(int count = -1)
    {
        // CreationService creationService = new CreationService();
        // Creation creation = creationService.GetCreationAsync().Result;


        return Task.FromResult(true);
    }
}