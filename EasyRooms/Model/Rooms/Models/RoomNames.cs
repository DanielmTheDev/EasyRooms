using Newtonsoft.Json;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace EasyRooms.Model.Rooms.Models;

public class RoomNames
{
    public string PartnerRoomsString { get; set; }
    public string RoomsString { get; set; }
    public string MassagesForSpecificRooms { get; set; }

    [JsonIgnore]
    public List<string> AllRoomsAsList => RoomsString.Split("\r\n").ToList();

    [JsonIgnore]
    public List<string> PartnerRoomsRoomsAsList => PartnerRoomsString.Split("\r\n").Where(room => !string.IsNullOrEmpty(room)).ToList();

    [JsonIgnore] public List<string> MassagesForSpecificRoomsAsList => MassagesForSpecificRooms.Split("\r\n").Where(room => !string.IsNullOrEmpty(room)).ToList();


    public RoomNames(
        string roomsString = "Wintergarten\r\nKosmetikraum\r\nRaum1\r\nRaum2\r\nFußpflegeraum\r\nSpecialRoom1\r\nSpecialRoom2\r\nSpecialRoom3\r\nSpecialRoom4",
        string partnerRoomsString = "Wintergarten",
        string massagesForSpecificRooms = "KRÄUTER\r\nSTONE")
    {
        RoomsString = roomsString;
        PartnerRoomsString = partnerRoomsString;
        MassagesForSpecificRooms = massagesForSpecificRooms;
        GuardRooms();
    }

    private void GuardRooms()
    {
        _ = PartnerRoomsRoomsAsList.All(partnerRoom => AllRoomsAsList.Contains(partnerRoom))
            ? default(object)
            : throw new ArgumentException(nameof(PartnerRoomsString));
    }
}