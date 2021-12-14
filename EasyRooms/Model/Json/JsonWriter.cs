using System.Collections.Generic;
using System.IO;
using EasyRooms.Model.Rooms.Models;
using Newtonsoft.Json;

namespace EasyRooms.Model.Json;

public static class JsonWriter
{
    public static void WriteJson(IEnumerable<Room>? filledRooms)
    {
        var serializedRooms = JsonConvert.SerializeObject(filledRooms, Formatting.Indented);
        File.WriteAllText(@"C:\Repos\EasyRooms\EasyRooms.Tests\IntegrationTests\TestData\realFlowRooms.json", serializedRooms);
    }
}