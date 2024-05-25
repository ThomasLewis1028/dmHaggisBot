using Microsoft.Extensions.Logging;
using SWNUniverseGenerator.Database;

namespace SWNUniverseGenerator.CreationTools;

public class ContextService <T>
{
    protected readonly UniverseContext _context;
    protected readonly ILogger<T> _logger;
    
    public ContextService(UniverseContext context)
    {
        _context = context;
        var loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        _logger = loggerFactory.CreateLogger<T>();
    }
}