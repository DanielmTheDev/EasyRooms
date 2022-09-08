using EasyRooms.Model.CommonExtensions;
using EasyRooms.Model.Rooms.Interfaces;

namespace EasyRooms.Model.Rooms.Implementations;

public class RoomOccupationsFiller : IRoomOccupationsFiller
{
    private readonly ITherapyFiller _therapyFiller;
    private readonly IRoomListCreator _roomListCreator;

    public RoomOccupationsFiller(ITherapyFiller therapyFiller, IRoomListCreator roomListCreator)
    {
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
        _therapyFiller.AddAllTherapies(rooms, orderedRows, bufferInMinutes, roomNames);
        rooms.ForEach(room => room.OrderOccupations());
        FillAllAdjacentProperties(rooms);
        return rooms;
    }

    private static void FillAllAdjacentProperties(IEnumerable<Room> rooms)
        => rooms.ForEach(FillAdjacentProperties);

    private static void FillAdjacentProperties(Room room)
    {
        for (var i = 1; i < room.Occupations.Count - 1; i++)
        {
            room.Occupations[i].TouchesAdjacent = TouchesPreviousOrNextOccupation(room, i);
        }
    }

    private static bool TouchesPreviousOrNextOccupation(Room room, int i)
        => TouchEachOther(room.Occupations[i], room.Occupations[i + 1]) || TouchEachOther(room.Occupations[i - 1], room.Occupations[i]);

    private static bool TouchEachOther(Occupation occupation1, Occupation occupation2)
    {
        var haveSamePatient = occupation1.Patient.EqualsInvariant(occupation2.Patient);
        return !haveSamePatient && occupation1.Touches(occupation2);
    }

    private static IEnumerable<Row> OrderRows(IEnumerable<Row> rows)
        => rows.OrderBy(row => TimeSpan.Parse(row.StartTime))
            .ThenBy(row => row.Duration);
}