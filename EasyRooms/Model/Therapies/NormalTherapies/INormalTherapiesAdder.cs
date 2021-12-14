using System.Collections.Generic;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;

namespace EasyRooms.Model.Therapies.NormalTherapies;

public interface INormalTherapiesAdder
{
    void AddNormalTherapies(List<Room> rooms, List<Row> orderedRows, int bufferInMinutes);
}