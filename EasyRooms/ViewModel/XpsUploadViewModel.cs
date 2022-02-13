using EasyRooms.Model.Buffer.Implementations;
using EasyRooms.Model.DayPlan.Interfaces;
using EasyRooms.Model.FileDialog.Interfaces;
using EasyRooms.Model.Json;
using EasyRooms.Model.Pdf.Interfaces;
using EasyRooms.Model.Rooms.Interfaces;
using EasyRooms.Model.Validation.Exceptions;
using EasyRooms.Model.Validation.Interfaces;
using EasyRooms.ViewModel.Commands;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace EasyRooms.ViewModel;

public class XpsUploadViewModel : BindableBase
{
    public RelayCommand CalculateOccupationsCommand { get; set; }
    public RelayCommand ChooseFileCommand { get; set; }

    private readonly IDayPlanParser _dayPlanParser;
    private readonly IFileDialogOpener _fileDialogOpener;
    private readonly IRoomOccupationsFiller _occupationsFiller;
    private readonly IRoomsValidator _validator;
    private readonly IPdfWriter _pdfWriter;
    private readonly IRoomNamesService _roomNamesService;
    private readonly IBufferService _bufferService;

    private string? _fileName;

    public XpsUploadViewModel(
        IRoomOccupationsFiller occupationsFiller,
        IDayPlanParser dayPlanParser,
        IFileDialogOpener fileDialogOpener,
        IRoomsValidator validator,
        IPdfWriter pdfWriter,
        IRoomNamesService roomNamesService, IBufferService bufferService)
    {
        CalculateOccupationsCommand = new RelayCommand(CalculateOccupations, CanCalculateOccupations);
        ChooseFileCommand = new RelayCommand(OpenFileDialog);
        _roomNamesService = roomNamesService;
        _bufferService = bufferService;
        _fileName = @"C:\Repos\EasyRooms\EasyRooms.Tests\IntegrationTests\TestData\PlanWithPartnerMassages.xps";
        _occupationsFiller = occupationsFiller;
        _dayPlanParser = dayPlanParser;
        _fileDialogOpener = fileDialogOpener;
        _validator = validator;
        _pdfWriter = pdfWriter;
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
        var filledRooms = _occupationsFiller.FillRoomOccupations(rows, _roomNamesService.Rooms, int.Parse(_bufferService.Buffer)).ToList();
        Validate(filledRooms);
        _pdfWriter.Write(filledRooms);
        JsonWriter.Write(filledRooms, @"C:\Repos\EasyRooms\EasyRooms.Tests\IntegrationTests\TestData\realFlowRooms.json");
    }

    private void Validate(IEnumerable<Room> filledRooms)
        => _ = _validator.IsValid(filledRooms, _roomNamesService.Rooms)
            ? default(object)
            : throw new RoomsValidationException();
}