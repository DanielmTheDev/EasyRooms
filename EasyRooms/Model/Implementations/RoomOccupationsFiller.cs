using System;
using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Interfaces;
using EasyRooms.Model.Models;

namespace EasyRooms.Model.Implementations;

public class RoomOccupationsFiller : IRoomOccupationsFiller
{
    private readonly IOccupationCreationDataProvider _occupationCreationDataProvider;
    private readonly ITherapyFiller _therapyFiller;
    private readonly IRoomListCreator _roomListCreator;

    public RoomOccupationsFiller(
        IOccupationCreationDataProvider occupationKeyInformationExtractor,
        ITherapyFiller therapyFiller,
        IRoomListCreator roomListCreator)
    {
        _occupationCreationDataProvider = occupationKeyInformationExtractor;
        _therapyFiller = therapyFiller;
        _roomListCreator = roomListCreator;
    }

    public IEnumerable<Room> FillRoomOccupations(IEnumerable<Row> rows, RoomNames roomNames, int bufferInMinutes = 0)
    {
        var orderedRows = OrderRows(rows).ToList();
        return CreateRooms(roomNames, orderedRows, bufferInMinutes);
    }

    private IEnumerable<Room> CreateRooms(RoomNames roomNames, List<Row> orderedRows, int bufferInMinutes)
    {
        var rooms = _roomListCreator.CreateRooms(roomNames);
        _therapyFiller.AddPartnerTherapies(rooms, orderedRows, bufferInMinutes);
        _therapyFiller.AddNormalTherapies(rooms, orderedRows, bufferInMinutes);
        return rooms;
    }

    private static IOrderedEnumerable<Row> OrderRows(IEnumerable<Row> rows)
        => rows.OrderBy(row => TimeSpan.Parse(row.StartTime.Trim('(', ')')))
            .ThenBy(row => int.Parse(row.Duration));
}