using System.Collections.Generic;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;

namespace EasyRooms.Model.Therapies.RoomSpecificTherapies;

public interface IRoomSpecificMassagesAdder
{
    void AddRoomSpecificMassages(IEnumerable<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames);
}