

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

using Newtonsoft.Json;

namespace EasyRooms.Model.Rooms.Models;

public class RoomNames
{
    public string PartnerRoomString { get; set; }
    public string RoomsString { get; set; }
    public string RoomsForSpecificMassages { get; set; }
    public string MassagesForSpecificRooms { get; set; }

    [JsonIgnore]
    public List<string> AllRoomsAsList => RoomsString.Split("\r\n").ToList();
    [JsonIgnore]
    public List<string> PartnerRoomsRoomsAsList => PartnerRoomString.Split("\r\n").ToList();
    [JsonIgnore]
    public List<string> RoomsForSpecificMassagesAsList => RoomsForSpecificMassages.Split("\r\n").ToList();
    public List<string> MassagesForSpecificRoomsAsList => MassagesForSpecificRooms.Split("\r\n").ToList();


    public RoomNames(
        string roomsString = "Wintergarten\r\nKosmetikraum\r\nRaum1\r\nRaum2\r\nFußpflegeraum\r\nSpecialRoom1\r\nSpecialRoom2\r\nSpecialRoom3\r\nSpecialRoom4",
        string partnerRoomString = "Wintergarten",
        string roomsForSpecificMassages = "SpecialRoom1\r\nSpecialRoom2\r\nSpecialRoom3\r\nSpecialRoom4",
        string massagesForSpecificRooms = "KRÄUTER\r\nSTONE")
    {
        RoomsString = roomsString;
        PartnerRoomString = partnerRoomString;
        RoomsForSpecificMassages = roomsForSpecificMassages;
        MassagesForSpecificRooms = massagesForSpecificRooms;
        GuardRooms();
    }

    private void GuardRooms()
    {
        _ = PartnerRoomsRoomsAsList.All(partnerRoom => AllRoomsAsList.Contains(partnerRoom))
            ? default(object)
            : throw new ArgumentException(nameof(PartnerRoomString));
        _ = RoomsForSpecificMassagesAsList.All(specificRoom => AllRoomsAsList.Contains(specificRoom))
            ? default(object)
            : throw new ArgumentException(nameof(RoomsForSpecificMassages));
    }
}