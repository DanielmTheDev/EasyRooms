using System.IO;
using EasyRooms.Model.Rooms.Interfaces;
using Newtonsoft.Json;
using JsonWriter = EasyRooms.Model.Json.JsonWriter;

namespace EasyRooms.Model.Rooms.Models;

public class RoomNamesService : IRoomNamesService
{
    public RoomNames Rooms { get; set; }
    private const string SaveRoomsPath = @".\saved_data\serializedRooms.json";

    public RoomNamesService()
        => Rooms = LoadRooms();

    public void SaveRooms()
        => JsonWriter.Write(Rooms, SaveRoomsPath);

    private static RoomNames LoadRooms()
        => File.Exists(SaveRoomsPath)
            ? JsonConvert.DeserializeObject<RoomNames>(File.ReadAllText(SaveRoomsPath)) ?? new RoomNames()
            : new RoomNames();
}