namespace EasyRooms.Model.Persistence.Models;

public class SavedOptions
{
    public SavedRooms Rooms { get; } = new();
    public int Buffer { get; set; }
}