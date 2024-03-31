using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.Database;

namespace SWNBlazorApp.Data;

public class DataService <T>
{
    protected readonly UniverseContext _context;
    protected readonly ILogger<T> _logger;
    public DataService(UniverseContext context, ILogger<T> logger)

    {
        _context = context;
        _logger = logger;
    }
}