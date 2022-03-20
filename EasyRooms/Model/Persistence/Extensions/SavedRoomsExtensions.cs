using EasyRooms.Model.Persistence.Models;

namespace EasyRooms.Model.Persistence.Extensions;

public static class SavedRoomsExtensions
{
    public static RoomNames ToRoomNames(this SavedRooms savedRooms)
        => new(savedRooms.Rooms, savedRooms.PartnerRooms, savedRooms.MassagesForSpecificRooms);
}