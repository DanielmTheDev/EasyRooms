using System;
using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Interfaces;
using EasyRooms.Model.Models;

namespace EasyRooms.Model.Implementations;

public class RoomOccupationsFiller : IRoomOccupationsFiller
{
    private readonly IOccupationKeyInformationExtractor _occupationKeyInformationExtractor;
    private readonly IPartnerRoomFiller _partnerRoomFiller;

    public RoomOccupationsFiller(IOccupationKeyInformationExtractor occupationKeyInformationExtractor, IPartnerRoomFiller partnerRoomFiller)
    {
        _occupationKeyInformationExtractor = occupationKeyInformationExtractor;
        _partnerRoomFiller = partnerRoomFiller;
    }

    public IEnumerable<Room> FillRoomOccupations(IEnumerable<Row> rows, RoomNames roomNames, int bufferInMinutes = 0)
    {
        var orderedRows = OrderRows(rows).ToList();
        return CreateRooms(roomNames, orderedRows, bufferInMinutes);
    }

    private IEnumerable<Room> CreateRooms(RoomNames roomNames, List<Row> orderedRows, int bufferInMinutes)
    {
        var rooms = roomNames.AllRoomsAsList.Select((name, i) => new Room(name, i)).ToList();
        SetPartnerRooms(roomNames, rooms);
        _partnerRoomFiller.AddPartnerTherapies(rooms, orderedRows, bufferInMinutes);
        AddNormalTherapies(rooms, orderedRows, bufferInMinutes);
        return rooms;
    }

    private static void SetPartnerRooms(RoomNames roomNames, List<Room> rooms)
        => roomNames.PartnerRoomsRoomsAsList.ToList()
            .ForEach(partnerRoom => rooms
            .Single(room => string.Equals(room.Name, partnerRoom, StringComparison.OrdinalIgnoreCase)).IsPartnerRoom = true);

    private void AddNormalTherapies(List<Room> rooms, List<Row> orderedRows, int bufferInMinutes)
        => orderedRows.ForEach(row => AddOccupation(row, rooms, bufferInMinutes));

    private void AddOccupation(Row row, List<Room> rooms, int bufferInMinutes)
    {
        var (startTime, endTime, freeRoom) = _occupationKeyInformationExtractor.GetOccupationInformation(row.StartTime, row.Duration, bufferInMinutes, rooms);
        freeRoom.AddOccupation(new Occupation(row.Therapist, row.Patient, row.TherapyShort, row.TherapyLong, startTime, endTime));
    }

    private static IOrderedEnumerable<Row> OrderRows(IEnumerable<Row> rows)
        => rows.OrderBy(row => TimeSpan.Parse(row.StartTime.Trim('(', ')')))
            .ThenBy(row => int.Parse(row.Duration));
}
