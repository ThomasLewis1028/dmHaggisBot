using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class CharacterService : DataService
{
    public CharacterService(UniverseContext context) : base(context)
    {

    }
    
    public Task<List<Character>> GetCharactersAsync(string universeId)
    {
        List<Character> result;
        var repo = new Repository<Character>(Context);
        var entityList = repo.Search(c => c.UniverseId == universeId).ToList();
        result = entityList.Cast<Character>().ToList();
      
        return Task.FromResult(result);
    }

    public Task<bool> CreateCharacterAsync(int count = -1)
    {
        // CreationService creationService = new CreationService();
        // Creation creation = creationService.GetCreationAsync().Result;

        
        return Task.FromResult(true);
    } 
}