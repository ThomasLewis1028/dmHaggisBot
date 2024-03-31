using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class CrewMemberService : DataService<CrewMemberService>
{
    public CrewMemberService(UniverseContext context, ILogger<CrewMemberService> logger) : base(context, logger)
    {

    }
    
    public Task<CrewMember> GetCrewMemberAsync(string charId)
    {
        var repo = new Repository<CrewMember>(_context);
        var result = (CrewMember)repo.Search(cm => cm.CharacterId == charId).FirstOrDefault()!;
        
        return Task.FromResult(result);
    }
    
    public Task<List<CrewMember>> GetCrewMembersAsync(string universeId)
    {
        List<CrewMember> result;
        var repo = new Repository<CrewMember>(_context);
        var entityList = repo.Search(c => c.UniverseId == universeId).ToList();
        result = entityList.Cast<CrewMember>().ToList();
    
        return Task.FromResult(result);
    }

    public Task<int> GetCrewMemberCount(string shipId)
    {
        var repo = new Repository<CrewMember>(_context);
        var result = repo.Count(cm => cm.ShipId == shipId);

        return Task.FromResult(result);
    }
}