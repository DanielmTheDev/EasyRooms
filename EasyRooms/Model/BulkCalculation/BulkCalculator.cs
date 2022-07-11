using System.IO;
using EasyRooms.Model.BulkCalculation.Models;
using EasyRooms.Model.CommonExtensions;
using EasyRooms.Model.Dialogs.Interfaces;
using EasyRooms.Model.Rooms.Interfaces;

namespace EasyRooms.Model.BulkCalculation;

public class BulkCalculator : IBulkCalculator
{
    private readonly IFilledRoomsProvider _roomsProvider;
    private readonly IMessageBoxShower _messageBoxShower;
    private const string XpsExtension = ".xps";

    public BulkCalculator(IFilledRoomsProvider roomsProvider, IMessageBoxShower messageBoxShower)
    {
        _roomsProvider = roomsProvider;
        _messageBoxShower = messageBoxShower;
    }

    public void Calculate(string path)
    {
        var results = new DirectoryInfo(path)
            .EnumerateFiles()
            .Where(file => file.Extension.EqualsInvariant(XpsExtension))
            .Aggregate(new ProcessingResults(), (results, file) => CalculateAndRecordResult(file, results));
        ShowDialog(results);
    }

    private ProcessingResults CalculateAndRecordResult(FileSystemInfo file, ProcessingResults results)
    {
        try
        {
            _roomsProvider.Get(file.FullName);
            results.SuccessfullyProcessed.Add(file.Name);
        }
        catch (Exception exception)
        {
            results.FaultyProcessed.Add(new(file.Name, exception));
        }

        return results;
    }

    private static string AssembleMessage(ProcessingResults results)
        => $"{AssembleSuccessMessage(results.SuccessfullyProcessed)}" +
           $"{Environment.NewLine}{Environment.NewLine}" +
           $"{AssembleErrorMessage(results.FaultyProcessed)}";

    private static string AssembleSuccessMessage(IList<string> successfullyProcessed)
        => successfullyProcessed.Any()
            ? $"Folgende Dateien erfolgreich prozessiert:{Environment.NewLine}{Environment.NewLine}" +
              $"{string.Join(Environment.NewLine, successfullyProcessed)}"
            : string.Empty;

    private static string AssembleErrorMessage(IList<FilenameWithException> faulty)
        => faulty.Any()
            ? $"Folgende Dateien konnten nicht prozessiert werden:{Environment.NewLine}{Environment.NewLine}" +
              $"{string.Join($"{Environment.NewLine}{Environment.NewLine}", faulty)}"
            : string.Empty;

    private void ShowDialog(ProcessingResults results)
    {
        var message = AssembleMessage(results);
        if (results.FaultyProcessed.Any())
            _messageBoxShower.Error(message);
        else
            _messageBoxShower.Success(message);
    }
}