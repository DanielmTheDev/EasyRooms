using System;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace EasyRooms.Model.Rooms.Models;

public class RoomNames
{
    public string PartnerRoomString { get; set; }
    public string RoomsString { get; set; }
    public string RoomsForSpecificMassages { get; set; }
    public string MassagesForSpecificRooms { get; set; }

    public List<string> AllRoomsAsList => RoomsString.Split('\n').ToList();
    public List<string> PartnerRoomsRoomsAsList => PartnerRoomString.Split('\n').ToList();
    public List<string> RoomsForSpecificMassagesAsList => RoomsForSpecificMassages.Split('\n').ToList();
    public List<string> MassagesForSpecificRoomsAsList => MassagesForSpecificRooms.Split('\n').ToList();


    public RoomNames(
        string roomsString = "Wintergarten\nKosmetikraum\nRaum1\nRaum2\nFußpflegeraum",
        string partnerRoomString = "Wintergarten",
        string roomsForSpecificMassages = "Wintergarten\nKosmetikraum\nRaum1\nRaum2\nFußpflegeraum",
        string massagesForSpecificRooms = "KRÄUTER\nSTONE")
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