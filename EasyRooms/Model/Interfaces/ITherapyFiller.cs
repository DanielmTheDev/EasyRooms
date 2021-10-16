using System.Collections.Generic;
using EasyRooms.Model.Models;

namespace EasyRooms.Model.Interfaces;

public interface ITherapyFiller
{
    void AddAllTherapies(List<Room> rooms, List<Row> orderedRows, int bufferInMinutes);
}