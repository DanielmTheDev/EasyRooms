using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Occupations;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;
using EasyRooms.Model.Validation;

namespace EasyRooms.Model.Therapies.Preparations;

internal class PreparationsAdder : IPreparationsAdder
{
    private readonly IOccupationsAdder _occupationsAdder;

    public PreparationsAdder(IOccupationsAdder occupationsAdder)
        => _occupationsAdder = occupationsAdder;

    public void AddPreparations(List<Room> rooms, List<Row> orderedRows)
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