using System.Collections.Generic;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;

namespace EasyRooms.Model.Therapies;

public interface ITherapyFiller
{
    void AddAllTherapies(List<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames);
}