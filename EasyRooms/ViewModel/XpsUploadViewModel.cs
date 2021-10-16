using System;
using System.IO;
using EasyRooms.Model.DayPlan;
using EasyRooms.Model.FileDialog;
using EasyRooms.Model.Rooms;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.ViewModel.Commands;
using Newtonsoft.Json;

namespace EasyRooms.ViewModel;

public class XpsUploadViewModel : BindableBase
{
    public RoomNames Rooms { get; set; }
    public RelayCommand CalculateOccupationsCommand { get; private set; }
    public RelayCommand ChooseFileCommand { get; private set; }

    private readonly IDayPlanParser _dayPlanParser;
    private readonly IFileDialogOpener _fileDialogOpener;
    private readonly IRoomOccupationsFiller _occupationsFiller;

    private string? _fileName;
    private readonly int _buffer = 1;

    public XpsUploadViewModel(IRoomOccupationsFiller occupationsFiller, IDayPlanParser dayPlanParser, IFileDialogOpener fileDialogOpener)
    {
        HookUpCommands();
        Rooms = new RoomNames();
        _occupationsFiller = occupationsFiller;
        _dayPlanParser = dayPlanParser;
        _fileDialogOpener = fileDialogOpener;
    }

    private void HookUpCommands()
    {
        CalculateOccupationsCommand = new RelayCommand(CalculateOccupations, CanCalculateOccupations);
        ChooseFileCommand = new RelayCommand(OpenFileDialog);
    }

    private void OpenFileDialog()
    {
        _fileName = _fileDialogOpener.GetFileNameFromDialog();
        CalculateOccupationsCommand.RaiseCanExecuteChanged();
    }

    private bool CanCalculateOccupations()
        => !string.IsNullOrEmpty(_fileName);

    private void CalculateOccupations()
    {
        _ = _fileName ?? throw new ArgumentNullException(nameof(_fileName));
        var rows = _dayPlanParser.ParseDayPlan(_fileName);
        var filledRooms = _occupationsFiller.FillRoomOccupations(rows, Rooms, _buffer);
        var serializedRooms = JsonConvert.SerializeObject(filledRooms, Formatting.Indented);
        File.WriteAllText(@"C:\Repos\EasyRooms\EasyRooms.Tests\IntegrationTests\TestData\realFlowRooms.json", serializedRooms);
    }
}
