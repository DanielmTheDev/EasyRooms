using System.IO;
using Newtonsoft.Json;

namespace EasyRooms.Model.Json;

public static class JsonWriter
{
    public static void Write(object filledRooms, string path)
    {
        var serializedRooms = JsonConvert.SerializeObject(filledRooms, Formatting.Indented);
        Directory.CreateDirectory(Path.GetDirectoryName(path) ?? string.Empty);
        File.WriteAllText(path, serializedRooms);
    }
}