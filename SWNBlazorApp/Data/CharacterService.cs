using SWNUniverseGenerator;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class CharacterService
{
    public Task<List<Character>> GetCharactersAsync(Universe universe)
    {
        return Task.FromResult(universe.Characters);
    }

    public Task<bool> CreateCharacterAsync(int count = -1)
    {
        // CreationService creationService = new CreationService();
        // Creation creation = creationService.GetCreationAsync().Result;

        
        return Task.FromResult(true);
    } 
}