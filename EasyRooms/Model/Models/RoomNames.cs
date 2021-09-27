using System.Collections.Generic;

namespace EasyRooms.ViewModel;

public class RoomNames
{
    public string RoomsString { get; set; }
    public string PartnerRoomName { get; set; }

    public IEnumerable<string> RoomsAsList => RoomsString.Split('\n');

    public RoomNames()
    {
        RoomsString = "Raum1\nRaum2\nRaum3\nRaum4\nRaum5\n";
        PartnerRoomName = "Raum1";
    }
}
