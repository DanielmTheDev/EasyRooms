using EasyRooms.Model.Rooms.Interfaces;

namespace EasyRooms.Model.Rooms.Implementations;

public class RoomListCreator : IRoomListCreator
{
    public List<Room> CreateRooms(RoomNames roomNames)
    {
        var rooms = GetRoomsWithAddedEmptyOne(roomNames);
        SetPartnerRoomProperty(roomNames, rooms);
        return rooms;
    }

    private static List<Room> GetRoomsWithAddedEmptyOne(RoomNames roomNames)
    {
        var roomsAsListWithEmpty = roomNames.AllRoomsAsList;
        roomsAsListWithEmpty.Add(string.Empty);
        return roomsAsListWithEmpty
            .Select((name, i) => new Room(name))
            .ToList();
    }

    private static void SetPartnerRoomProperty(RoomNames roomNames, IReadOnlyCollection<Room> rooms)
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