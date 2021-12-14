using System.Collections.Generic;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;

namespace EasyRooms.Model.Therapies.Preparations;

public interface IPreparationsAdder
{
    void AddPreparations(List<Room> rooms, List<Row> orderedRows);
}