﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWNUniverseGenerator;
using SWNUniverseGenerator.CreationTools;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class UniverseService : DataService
{
    public UniverseService(UniverseContext context) : base(context)
    {

    }
    
    // public Task<Boolean> SetUniverse(Universe universe)
    // {
    //     _universe = universe;
    //     return Task.FromResult(true);
    // }
    
    public Task<Universe> GetUniverseAsync(string universeID)
    {
        Universe result;
        var universeRepo = new Repository<Universe>(Context);
        result = universeRepo.GetById(universeID);
        
        return Task.FromResult(result);
    }

    public Task<List<Universe>> GetUniverseListAsync()
    {
        List<Universe> result;
        var repo = new Repository<Universe>(Context);
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
        var repo = new Repository<Planet>(Context);
        result = repo.Count(c => c.UniverseId == universeId);
    
        return Task.FromResult(result);
    }

    public Task<int>  GetStarCount(string universeId)
    {
        int result;
        var repo = new Repository<Star>(Context);
        result = repo.Count(c => c.UniverseId == universeId);
    
        return Task.FromResult(result);
    }

    public Task<int>  GetShipCount(string universeId)
    {
        int result;
        var repo = new Repository<Ship>(Context);
        result = repo.Count(c => c.UniverseId == universeId);
    
        return Task.FromResult(result);
    }

    public Task<int>  GetCharCount(string universeId)
    {
        int result;
        var repo = new Repository<Character>(Context);
        result = repo.Count(c => c.UniverseId == universeId);
    
        return Task.FromResult(result);
    }
}

