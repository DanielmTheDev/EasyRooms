using System.Collections.Generic;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;

namespace EasyRooms.Model.Rooms
{
    public interface IRoomOccupationsFiller
    {
        IEnumerable<Room> FillRoomOccupations(IEnumerable<Row> rows, RoomNames roomNames, int bufferInMinutes = 0);
    }
    
}