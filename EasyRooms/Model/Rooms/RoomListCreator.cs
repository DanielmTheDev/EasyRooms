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
            return rooms;
        }

        private static void SetPartnerRoomProperty(RoomNames roomNames, List<Room> rooms)
            => roomNames.PartnerRoomsRoomsAsList.ToList()
                .ForEach(partnerRoom => rooms
                    .Single(room => string.Equals(room.Name, partnerRoom, StringComparison.OrdinalIgnoreCase))
                    .IsPartnerRoom = true);
    }
}