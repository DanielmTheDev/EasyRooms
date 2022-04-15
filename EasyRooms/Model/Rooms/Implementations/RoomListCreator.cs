using EasyRooms.Model.Rooms.Interfaces;

namespace EasyRooms.Model.Rooms.Implementations;

public class RoomListCreator : IRoomListCreator
{
    public IList<Room> CreateRooms(RoomNames roomNames)
    {
        var rooms = GetRoomsWithAddedEmptyOne(roomNames);
        SetPartnerRoomProperty(roomNames, rooms);
        return rooms;
    }

    private static IList<Room> GetRoomsWithAddedEmptyOne(RoomNames roomNames)
    {
        var roomsAsListWithEmpty = roomNames.AllRoomsAsList;
        roomsAsListWithEmpty.Add(string.Empty);
        return roomsAsListWithEmpty
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