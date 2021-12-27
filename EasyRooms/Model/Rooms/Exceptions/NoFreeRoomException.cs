using System;

namespace EasyRooms.Model.Rooms.Exceptions;

public class NoFreeRoomException : Exception
{
    public NoFreeRoomException() : base("No free room found.")
    {
    }
}