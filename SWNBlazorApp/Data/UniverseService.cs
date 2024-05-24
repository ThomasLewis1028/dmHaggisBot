using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWNUniverseGenerator;
using SWNUniverseGenerator.CreationTools;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class UniverseService : DataService<UniverseService>
{
    public UniverseService(UniverseContext context, ILogger<UniverseService> logger) : base(context, logger)
    {

    }
    
    public Task<Universe> GetUniverseAsync(string universeID)
    {
        _logger.LogInformation("Get Universe: " + universeID);
        Universe result;
        var universeRepo = new Repository<Universe>(_context);
        result = universeRepo.GetById(universeID);
        _logger.LogInformation("Got Universe: " + result.Name);
        return Task.FromResult(result);
    }

    public Task<List<Universe>> GetUniverseListAsync()
    {
        List<Universe> result;
        var repo = new Repository<Universe>(_context);
        result = repo.GetAll().ToList();
        
        return Task.FromResult(result);
    }

    public Task<bool> DeleteUniverseAsync(string universeID)
    {
        var result = new Creation().DeleteUniverse(universeID);
        
        if(File.Exists("wwwroot/images/starmaps/" + universeID + ".svg"))
            File.Delete("wwwroot/images/starmaps/" + universeID + ".svg");
        
        return Task.FromResult(result);
    }
    
    public Task<int>  GetPlanetCount(string universeId)
    {
        int result;
        var repo = new Repository<Planet>(_context);
        result = repo.Count(c => c.UniverseId == universeId);
    
        return Task.FromResult(result);
    }

    public Task<int>  GetStarCount(string universeId)
    {
        int result;
        var repo = new Repository<Star>(_context);
        result = repo.Count(c => c.UniverseId == universeId);
    
        return Task.FromResult(result);
    }

    public Task<int>  GetShipCount(string universeId)
    {
        int result;
        var repo = new Repository<Ship>(_context);
        result = repo.Count(c => c.UniverseId == universeId);
    
        return Task.FromResult(result);
    }

    public Task<int>  GetCharCount(string universeId)
    {
        int result;
        var repo = new Repository<Character>(_context);
        result = repo.Count(c => c.UniverseId == universeId);
    
        return Task.FromResult(result);
    }
    
    public Task<int>  GetPOICount(string universeId)
    {
        int result;
        var repo = new Repository<PointOfInterest>(_context);
        result = repo.Count(c => c.UniverseId == universeId);
    
        return Task.FromResult(result);
    }
}

