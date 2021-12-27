using System.Collections.Generic;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;
using EasyRooms.Model.Therapies.Interfaces;

namespace EasyRooms.Model.Therapies.Implementations;

public class TherapyFiller : ITherapyFiller
{
    private readonly IEnumerable<IMassagesAdder> _massageAdders;

    public TherapyFiller(IEnumerable<IMassagesAdder> massageAdders)
        => _massageAdders = massageAdders;

    public void AddAllTherapies(List<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
    {
        foreach (var massageAdder in _massageAdders)
        {
            massageAdder.Add(rooms, orderedRows, bufferInMinutes, roomNames);
        }
    }
}