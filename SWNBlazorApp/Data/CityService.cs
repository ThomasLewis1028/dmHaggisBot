using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class CityService : DataService<CityService>
{
    public CityService(UniverseContext context, ILogger<CityService> logger) : base(context, logger)
    {

    }
    
    public Task<City> GetCityAsync(string cityId)
    {
        City result;
        var repo = new Repository<City>(_context);
        result = repo.GetById(cityId);

        
        return Task.FromResult(result);
    }
    
    public Task<List<City>> GetCitiesAsync(string universeId)
    {
        List<City> result;
        var repo = new Repository<City>(_context);
        var entityList = repo.Search(c => c.UniverseId == universeId).ToList();
        result = entityList.Cast<City>().ToList();
        
        return Task.FromResult(result);
    }

    public Task<List<City>> GetCitiesByPlanetAsync(string planetId)
    {
        List<City> result;
        var repo = new Repository<City>(_context);
        var entityList = repo.Search(c => c.PlanetId == planetId).ToList();
        result = entityList.Cast<City>().ToList();
        
        return Task.FromResult(result);
    }
}