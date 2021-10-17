using System.Collections.Generic;
using EasyRooms.Model.Rooms.Models;

namespace EasyRooms.Model.Rooms
{
    public interface IFreeRoomFinder
    {
        FreeRoomWithTime CalculateOccupationCreationData(string startTimeString, string duration, int bufferInMinutes, IEnumerable<Room> rooms);
    }
}