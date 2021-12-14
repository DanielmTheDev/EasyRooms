using System.Collections.Generic;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;

namespace EasyRooms.Model.Occupations;

public interface IOccupationsAdder
{
    void AddToFreeRoom(IEnumerable<Room> rooms, int bufferInMinutes, params Row[] rows);
    void AddToSpecificRoom(IEnumerable<Room> rooms, string roomName, params Row[] rows);
}