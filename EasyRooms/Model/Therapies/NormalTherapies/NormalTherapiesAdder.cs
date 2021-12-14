using System.Collections.Generic;
using EasyRooms.Model.Occupations;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;

namespace EasyRooms.Model.Therapies.NormalTherapies;

public class NormalTherapiesAdder : INormalTherapiesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;

    public NormalTherapiesAdder(IOccupationsAdder occupationsAdder)
        => _occupationsAdder = occupationsAdder;

    public void AddNormalTherapies(List<Room> rooms, List<Row> orderedRows, int bufferInMinutes)
        => orderedRows.ForEach(row => _occupationsAdder.AddToFreeRoom(rooms, bufferInMinutes, row));
}