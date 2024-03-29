using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.Database;

namespace SWNBlazorApp.Data;

public class DataService
{
    protected readonly UniverseContext Context;

    public DataService(UniverseContext context)
    {
        Context = context;
    }
}