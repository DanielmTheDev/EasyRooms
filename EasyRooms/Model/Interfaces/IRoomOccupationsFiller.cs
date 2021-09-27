using System.Collections.Generic;
using EasyRooms.Model.Models;
using EasyRooms.ViewModel;

namespace EasyRooms.Model.Interfaces;

public interface IRoomOccupationsFiller
{
    IEnumerable<Room> FillRoomOccupations(IEnumerable<Row> rows, RoomNames roomNames, int bufferInMinutes = 0);
}
