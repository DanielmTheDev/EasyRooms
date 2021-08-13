using EasyRooms.Models;
using System.Collections.Generic;

namespace EasyRooms.Interfaces
{
    public interface IRoomOccupationsFiller
    {
        IEnumerable<Room> FillRoomOccupations(IEnumerable<Row> rows, IEnumerable<string> roomNames);
    }
}