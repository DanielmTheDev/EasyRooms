namespace EasyRooms.Model.Rooms.Models;

public record RoomsWithDate(IEnumerable<Room> Rooms, DateOnly Date);