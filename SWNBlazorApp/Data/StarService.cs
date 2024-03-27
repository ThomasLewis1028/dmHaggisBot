using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class StarService
{
    private readonly IDbContextFactory<UniverseContext> _contextFactory;

    public StarService(IDbContextFactory<UniverseContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public Task<List<Star>> GetStarsAsync(string universeId)
    {
        List<Star> result;
        using (var context = _contextFactory.CreateDbContext())
        {
            using (var repo = new Repository<Star>(context))
            {
                var entityList = repo.Search(c => c.UniverseId == universeId).ToList();
                result = entityList.Cast<Star>().ToList();
            }
        }
        return Task.FromResult(result);
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

    public Task<bool> CreateCharacterAsync(int count = -1)
    {
        // CreationService creationService = new CreationService();
        // Creation creation = creationService.GetCreationAsync().Result;

        
        return Task.FromResult(true);
    } 
}