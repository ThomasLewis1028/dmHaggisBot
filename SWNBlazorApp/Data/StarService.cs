using SWNUniverseGenerator;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class StarService
{
    public static Task<List<Star>> GetStarsAsync(Universe universe)
    {
        return Task.FromResult(universe.Stars);
    }
    
    public static Task<List<Zone>> GetZonesAsync(Universe universe)
    {
        return Task.FromResult(universe.Zones);
    }

    public Task<bool> CreateCharacterAsync(int count = -1)
    {
        // CreationService creationService = new CreationService();
        // Creation creation = creationService.GetCreationAsync().Result;

        
        return Task.FromResult(true);
    } 
}