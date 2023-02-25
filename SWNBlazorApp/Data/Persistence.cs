using System.Reflection;
using Newtonsoft.Json;

namespace SWNBlazorApp.Data;

public class Persistence
{
    public String CurrentUniverseName { get; set; }
}

public class SerializeClass
{
    public void SerializeData<T>(T generic)
    {
        // Set the path to the file and write it, overwriting the previous file if it exists.
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/persistence.json";
        using var file =
            File.CreateText(path);
        var serializer = new JsonSerializer();
        serializer.Serialize(file, generic);
    }
}