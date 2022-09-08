﻿using EasyRooms.Model.Rooms.Interfaces;

namespace EasyRooms.Model.Rooms.Implementations;

public class RoomListCreator : IRoomListCreator
{
    public IList<Room> CreateRooms(RoomNames roomNames)
    {
        var rooms = GetRoomsWithAddedEmptyOne(roomNames);
        SetPartnerRoomProperty(roomNames, rooms);
        return rooms;
    }

    // Empty room is used for Hausbeusche, Pause and similar pseudo-therapies
    private static IList<Room> GetRoomsWithAddedEmptyOne(RoomNames roomNames)
    {
        var rooms = roomNames.AllRoomsAsList;
        if(!rooms.Contains(string.Empty))
            rooms.Add(string.Empty);
        return rooms
            .Select(name => new Room(name))
            .ToList();
    }

    private static void SetPartnerRoomProperty(RoomNames roomNames, IList<Room> rooms)
        => roomNames.PartnerRoomsRoomsAsList
            .ForEach(partnerRoom =>
            {
                var room = rooms
                    .SingleOrDefault(room =>
                        string.Equals(room.Name, partnerRoom, StringComparison.OrdinalIgnoreCase));
                if (room is { })
                    room.IsPartnerRoom = true;
            });
}