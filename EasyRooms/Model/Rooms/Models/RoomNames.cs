using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyRooms.Model.Rooms.Models
{
    public class RoomNames
    {
        public string PartnerRoomString { get; set; }
        private string RoomsString { get; set; }
        private string RoomsForSpecificMassages { get; set; }
        private string MassagesForSpecificRooms { get; set; }

        public List<string> AllRoomsAsList => RoomsString.Split('\n').ToList();
        public List<string> PartnerRoomsRoomsAsList => PartnerRoomString.Split('\n').ToList();
        public List<string> RoomsForSpecificMassagesAsList => RoomsForSpecificMassages.Split('\n').ToList();

        public RoomNames(
            string roomsString = "Raum1\nRaum2\nRaum3\nRaum4\nRaum5\nRaum6\nRaum7\nRaum8\n", 
            string partnerRoomString = "Raum1\nRaum2",
            string roomsForSpecificMassages = "Raum3\nRaum4\n",
            string massagesForSpecificRooms = "RÜCK40\nFUSS\n")
        {
            RoomsString = roomsString;
            PartnerRoomString = partnerRoomString;
            RoomsForSpecificMassages = roomsForSpecificMassages;
            MassagesForSpecificRooms = massagesForSpecificRooms;
            GuardPartnerRooms();
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