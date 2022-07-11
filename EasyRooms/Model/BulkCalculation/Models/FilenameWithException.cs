namespace EasyRooms.Model.BulkCalculation.Models;

public record FilenameWithException(string Name, Exception Exception)
{
    public override string ToString()
        => $"{Name}{Environment.NewLine}{Exception.Message}";
}