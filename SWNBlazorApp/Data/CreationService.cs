using SWNUniverseGenerator;

namespace SWNBlazorApp.Data;

public class CreationService
{
    public Task<Creation> GetCreationAsync()
    {
        Creation creation = new Creation();

        return Task.FromResult(creation);
    }
}