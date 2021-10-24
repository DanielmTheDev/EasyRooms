using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyRooms.Model.Rooms.Models
{
    public class RoomNames
    {
        public string RoomsString { get; set; }
        public string PartnerRoomString { get; set; }
        public IEnumerable<string> AllRoomsAsList => RoomsString.Split('\n');
        public IEnumerable<string> PartnerRoomsRoomsAsList => PartnerRoomString.Split('\n');

        public RoomNames(string roomsString = "Raum1\nRaum2\nRaum3\nRaum4\nRaum5\nRaum6\nRaum7\nRaum8\n", string partnerRoomString = "Raum1\nRaum2")
        {
            RoomsString = roomsString;
            PartnerRoomString = partnerRoomString;
        }

        private void GuardPartnerRooms()
        {
            if (!PartnerRoomsRoomsAsList.All(partnerRoom => AllRoomsAsList.Contains(partnerRoom)))
            {
                throw new ArgumentException(nameof(PartnerRoomString));
            }
        }
    }
    
}