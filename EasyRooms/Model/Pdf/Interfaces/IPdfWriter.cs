using System.Collections.Generic;
using EasyRooms.Model.Rooms.Models;

namespace EasyRooms.Model.Pdf.Interfaces;

public interface IPdfWriter
{
    void Write(IEnumerable<Room> filledRooms);
}