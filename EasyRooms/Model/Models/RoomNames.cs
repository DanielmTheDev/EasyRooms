using System.Collections.Generic;

namespace EasyRooms.Model.Models;

public class RoomNames
{
    public string RoomsString { get; set; }
    public string PartnerRoomString { get; set; }

    public IEnumerable<string> RoomsAsList => RoomsString.Split('\n');
    public IEnumerable<string> PartnerRoomsRoomsAsList => RoomsString.Split('\n');

    public RoomNames()
    {
        RoomsString = "Raum1\nRaum2\nRaum3\nRaum4\nRaum5\n";
        PartnerRoomString = "Raum1\nRoom2";
    }
}
