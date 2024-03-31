using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class CrewMemberService : DataService
{
    public CrewMemberService(UniverseContext context) : base(context)
    {

    }
    
    public Task<CrewMember> GetCrewMemberAsync(string charId)
    {
        var repo = new Repository<CrewMember>(Context);
        var result = (CrewMember)repo.Search(cm => cm.CharacterId == charId).FirstOrDefault()!;
        
        return Task.FromResult(result);
    }
    
    public Task<List<CrewMember>> GetCrewMembersAsync(string universeId)
    {
        List<CrewMember> result;
        var repo = new Repository<CrewMember>(Context);
        var entityList = repo.Search(c => c.UniverseId == universeId).ToList();
        result = entityList.Cast<CrewMember>().ToList();
    
        return Task.FromResult(result);
    }

    public Task<int> GetCrewMemberCount(string shipId)
    {
        var repo = new Repository<CrewMember>(Context);
        var result = repo.Count(cm => cm.ShipId == shipId);

        return Task.FromResult(result);
    }
}