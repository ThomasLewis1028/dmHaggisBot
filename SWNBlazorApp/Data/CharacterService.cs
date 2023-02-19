using SWNUniverseGenerator;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class CharacterService
{
    public Task<List<Character>> GetCharactersAsync(Universe universe)
    {
        return Task.FromResult(universe.Characters);
    }

    public Task<bool> CreateCharacterAsync()
    {
        CreationService creationService = new CreationService();
        Creation creation = creationService.GetCreationAsync().Result;

        // UniverseService
        
        return Task.FromResult(true);
    } 
}