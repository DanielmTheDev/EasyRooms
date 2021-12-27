using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Occupations.Interfaces;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;
using EasyRooms.Model.Therapies.Interfaces;
using EasyRooms.Model.Validation.Implementations;

namespace EasyRooms.Model.Therapies.Implementations;

internal class PreparationsAdder : IMassagesAdder
{
    private readonly IOccupationsAdder _occupationsAdder;

    public PreparationsAdder(IOccupationsAdder occupationsAdder)
        => _occupationsAdder = occupationsAdder;

    public void Add(IEnumerable<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
    {
        var preparations = orderedRows
            .Where(row => TherapyTypeProvider.IsPreparation(row.TherapyShort))
            .ToList();

        preparations.ForEach(preparation =>
        {
            _occupationsAdder.AddToSpecificRoom(rooms, string.Empty, preparation);
            orderedRows.Remove(preparation);
        });
    }
}