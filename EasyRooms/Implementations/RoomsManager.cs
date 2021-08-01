using EasyRooms.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace EasyRooms.Models
{
    public class RoomsManager
    {
        private IList<Room> Rooms;

        public RoomsManager(IEnumerable<string> roomNames) 
            => Rooms = roomNames.Select(name => new Room(name)).ToList();

        public void FillRoomOccupations(IEnumerable<Row> rows)
        {
            //first write test
        }
    }
}
