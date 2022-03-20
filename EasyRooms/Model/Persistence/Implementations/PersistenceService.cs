using System.IO;
using EasyRooms.Model.Persistence.Interfaces;
using EasyRooms.Model.Persistence.Models;
using Newtonsoft.Json;
using JsonWriter = EasyRooms.Model.Json.JsonWriter;

namespace EasyRooms.Model.Persistence.Implementations;

public class PersistenceService : IPersistenceService
{
    public SavedOptions SavedOptions { get; set; }
    private const string SaveFilePath = @".\saved_data\serializedRooms.json";

    public PersistenceService()
        => SavedOptions = LoadOptions();

    public void SaveOptions()
        => JsonWriter.Write(SavedOptions, SaveFilePath);

    private static SavedOptions LoadOptions()
        => File.Exists(SaveFilePath)
            ? JsonConvert.DeserializeObject<SavedOptions>(File.ReadAllText(SaveFilePath)) ?? new SavedOptions()
            : new SavedOptions();
}