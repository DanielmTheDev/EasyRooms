﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyRooms.Model.Rooms.Models
{
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
            string roomsString = "Raum1\nRaum2\nRaum3\nRaum4\nRaum5\nRaum6\nRaum7\nRaum8", 
            string partnerRoomString = "Raum1\nRaum2",
            string roomsForSpecificMassages = "Raum3\nRaum4",
            string massagesForSpecificRooms = "RÜCK40\nFUSS")
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
}