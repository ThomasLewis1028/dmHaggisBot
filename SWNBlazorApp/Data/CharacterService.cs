using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator;
using SWNUniverseGenerator.CreationTools;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class CharacterService : DataService<CharacterService>
{
    public CharacterService(UniverseContext context, ILogger<CharacterService> logger) : base(context, logger)
    {

    }

    public Task<Character> GetCharacterAsync(string charId)
    {
        Character result;
        var repo = new Repository<Character>(_context);
        result = repo.GetById(charId);
        
        return Task.FromResult(result);
    }
    
    public Task<List<Character>> GetCharactersAsync(string universeId)
    {
        List<Character> result;
        var repo = new Repository<Character>(_context);
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
    
    public void CreateCharacter(CharacterDefaultSettings characterDefaultSettings)
    {
        //TODO: Fix
        Creation creation = new Creation(_context);
        creation.CreateCharacter(characterDefaultSettings);
    }
}