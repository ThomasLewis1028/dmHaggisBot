using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class PointOfInterestService : DataService<PointOfInterestService>
{
    public PointOfInterestService(UniverseContext context, ILogger<PointOfInterestService> logger) : base(context, logger)
    {

    }
    
    public Task<PointOfInterest> GetPointOfInterestAsync(string pointofinterestID)
    {
        PointOfInterest result;
        var repo = new Repository<PointOfInterest>(_context);
        result = repo.GetById(pointofinterestID);

        
        return Task.FromResult(result);
    }
    
    public Task<List<PointOfInterest>> GetPointOfInterestsAsync(string universeId)
    {
        List<PointOfInterest> result;
        var repo = new Repository<PointOfInterest>(_context);
        var entityList = repo.Search(c => c.UniverseId == universeId).ToList();
        result = entityList.Cast<PointOfInterest>().ToList();
        
        return Task.FromResult(result);
    }

    
}