using System.Collections.Generic;
using EasyRooms.Model.Rooms.Models;

namespace EasyRooms.Model.Pdf
{
    public interface IPdfWriter
    {
        void Write(IEnumerable<Room> filledRooms);
    }
}