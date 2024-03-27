using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class CharacterService
{
    private readonly IDbContextFactory<UniverseContext> _contextFactory;

    public CharacterService(IDbContextFactory<UniverseContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public Task<List<Character>> GetCharactersAsync(string universeId)
    {
        List<Character> result;
        using (var context = _contextFactory.CreateDbContext())
        {
            using (var repo = new Repository<Character>(context))
            {
                var entityList = repo.Search(c => c.UniverseId == universeId).ToList();
                result = entityList.Cast<Character>().ToList();
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