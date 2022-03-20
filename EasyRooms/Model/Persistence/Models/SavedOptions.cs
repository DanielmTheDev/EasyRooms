namespace EasyRooms.Model.Persistence.Models;

public class SavedOptions
{
    public SavedRooms Rooms { get; set; } = new();
    public int Buffer { get; set; }
}