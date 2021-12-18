using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Occupations;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;
using EasyRooms.Model.Therapies.RoomSpecificTherapies;
using EasyRooms.Model.Validation;

namespace EasyRooms.Model.Therapies.Preparations;

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