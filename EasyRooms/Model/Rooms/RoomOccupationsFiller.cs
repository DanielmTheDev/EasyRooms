using System;
using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;
using EasyRooms.Model.Therapy;

namespace EasyRooms.Model.Rooms
{

    public class RoomOccupationsFiller : IRoomOccupationsFiller
    {
        private readonly ITherapyFiller _therapyFiller;
        private readonly IRoomListCreator _roomListCreator;

        public RoomOccupationsFiller(ITherapyFiller therapyFiller,  IRoomListCreator roomListCreator)
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
            return rooms;
        }

        private static IEnumerable<Row> OrderRows(IEnumerable<Row> rows)
            => rows.OrderBy(row => TimeSpan.Parse(row.StartTime.Trim('(', ')')))
                .ThenBy(row => int.Parse(row.Duration));
    }
}