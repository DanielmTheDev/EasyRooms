using System;
using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Rooms.Models;

namespace EasyRooms.Model.Rooms
{
    public class RoomListCreator : IRoomListCreator
    {
        public List<Room> CreateRooms(RoomNames roomNames)
        {
            var rooms = roomNames.AllRoomsAsList.Select((name, i) => new Room(name, i)).ToList();
            SetPartnerRoomProperty(roomNames, rooms);
            SetMassageSpecificRoomProperty(roomNames, rooms);
            return rooms;
        }

        private static void SetMassageSpecificRoomProperty(RoomNames roomNames, IReadOnlyCollection<Room> rooms)
            => roomNames.RoomsForSpecificMassagesAsList
                .ForEach(messageSpecificRoom => rooms
                    .Single(room => string.Equals(room.Name, messageSpecificRoom, StringComparison.OrdinalIgnoreCase))
                    .IsMassageSpecificRoom = true);

        private static void SetPartnerRoomProperty(RoomNames roomNames, IReadOnlyCollection<Room> rooms)
            => roomNames.PartnerRoomsRoomsAsList
                .ForEach(partnerRoom => rooms
                    .Single(room => string.Equals(room.Name, partnerRoom, StringComparison.OrdinalIgnoreCase))
                    .IsPartnerRoom = true);
    }
}