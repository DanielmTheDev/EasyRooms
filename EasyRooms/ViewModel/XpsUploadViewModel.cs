using System;
using System.IO;
using EasyRooms.Model.Interfaces;
using EasyRooms.ViewModel.Commands;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace EasyRooms.ViewModel;

public class XpsUploadViewModel : BindableBase
{
    public string RoomsString { get; set; }
    public string PartnerRoomString { get; set; }
    public RelayCommand CalculateOccupationsCommand { get; private set; }
    public RelayCommand ChooseFileCommand { get; private set; }

    private readonly IDayPlanParser _dayPlanParser;
    private readonly IRoomOccupationsFiller _occupationsFiller;

    private string? _fileName;
    private readonly int _buffer = 10;

    public XpsUploadViewModel(IRoomOccupationsFiller occupationsFiller, IDayPlanParser dayPlanParser)
    {
        RoomsString = "Raum1\nRaum2\nRaum3\nRaum4\nRaum5\n";
        CalculateOccupationsCommand = new RelayCommand(CalculateOccupations, CanCalculateOccupations);
        ChooseFileCommand = new RelayCommand(OpenFileDialog);
        _occupationsFiller = occupationsFiller;
        _dayPlanParser = dayPlanParser;
    }

    private void OpenFileDialog()
    {
        var dialog = new OpenFileDialog
        {
            DefaultExt = ".xps",
            Filter = "XPS Files (*.xps)|*.xps"
        };

        var result = dialog.ShowDialog();

        if (result == true)
        {
            _fileName = dialog.FileName;
        }
        CalculateOccupationsCommand.RaiseCanExecuteChanged();
    }

    private bool CanCalculateOccupations()
        => !string.IsNullOrEmpty(_fileName);

    private void CalculateOccupations()
    {
        _ = _fileName ?? throw new ArgumentNullException(nameof(_fileName));
        var rows = _dayPlanParser.ParseDayPlan(_fileName);
        var roomNames = RoomsString.Split('\n');
        var partnerRoomNames = PartnerRoomString.Split('n');
        var filledRooms = _occupationsFiller.FillRoomOccupations(rows, roomNames, _buffer);
        var serializedRooms = JsonConvert.SerializeObject(filledRooms, Formatting.Indented);
        File.WriteAllText(@"C:\Repos\EasyRooms\EasyRooms.Tests\IntegrationTests\TestData\realFlowRooms.json", serializedRooms);
    }
}
