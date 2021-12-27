using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Occupations.Interfaces;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;
using EasyRooms.Model.Therapies.Interfaces;

namespace EasyRooms.Model.Therapies.Implementations;

public class RoomSpecificMassagesAdder : IMassagesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;

    public RoomSpecificMassagesAdder(IOccupationsAdder occupationsAdder)
        => _occupationsAdder = occupationsAdder;

    public void Add(IEnumerable<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
    {
        var roomSpecificMassages = orderedRows
            .Where(row => roomNames.MassagesForSpecificRoomsAsList.Contains(row.TherapyShort))
            .GroupBy(row => (row.StartTime, row.Duration))
            .ToList();

        var specificRooms = rooms.Where(room => room.IsMassageSpecificRoom);

        roomSpecificMassages.ForEach(grouping =>
        {
            grouping.ToList().ForEach(row => _occupationsAdder.AddToFreeRoom(specificRooms, bufferInMinutes, row));
            grouping.ToList().ForEach(row => orderedRows.Remove(row));
        });
    }
}