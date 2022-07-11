namespace EasyRooms.Model.BulkCalculation.Models;

public record ProcessingResults
{
    public IList<string> SuccessfullyProcessed { get; } = new List<string>();
    public IList<FilenameWithException> FaultyProcessed { get; } = new List<FilenameWithException>();
}