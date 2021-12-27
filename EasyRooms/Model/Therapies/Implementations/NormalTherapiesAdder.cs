using System.Collections.Generic;
using EasyRooms.Model.Occupations.Interfaces;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;
using EasyRooms.Model.Therapies.Interfaces;

namespace EasyRooms.Model.Therapies.Implementations;

public class NormalTherapiesAdder : IMassagesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;

    public NormalTherapiesAdder(IOccupationsAdder occupationsAdder)
        => _occupationsAdder = occupationsAdder;

    public void Add(IEnumerable<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
        => orderedRows.ForEach(row => _occupationsAdder.AddToFreeRoom(rooms, bufferInMinutes, row));
}