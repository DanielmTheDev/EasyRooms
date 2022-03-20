using EasyRooms.Model.DayPlan.Interfaces;
using EasyRooms.Model.FileDialog.Interfaces;
using EasyRooms.Model.Json;
using EasyRooms.Model.Pdf.Interfaces;
using EasyRooms.Model.Persistence.Extensions;
using EasyRooms.Model.Persistence.Interfaces;
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
    private readonly IPersistenceService _persistenceService;

    private string? _fileName;

    public XpsUploadViewModel(
        IRoomOccupationsFiller occupationsFiller,
        IDayPlanParser dayPlanParser,
        IFileDialogOpener fileDialogOpener,
        IRoomsValidator validator,
        IPdfWriter pdfWriter,
        IPersistenceService persistenceService)
    {
        CalculateOccupationsCommand = new RelayCommand(CalculateOccupations, CanCalculateOccupations);
        ChooseFileCommand = new RelayCommand(OpenFileDialog);
        _persistenceService = persistenceService;
        _fileName = @"C:\Repos\EasyRooms\EasyRooms.Tests\IntegrationTests\TestData\16.07ultimativer Test.xps";
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
        GuardFileName();
        var rows = _dayPlanParser.ParseDayPlan(_fileName!);
        var roomNames = _persistenceService.SavedOptions.Rooms.ToRoomNames();
        var savedOptionsBuffer = _persistenceService.SavedOptions.Buffer;
        var filledRooms = _occupationsFiller.FillRoomOccupations(rows, roomNames, savedOptionsBuffer).ToList();
        Validate(filledRooms);
        _pdfWriter.Write(filledRooms);
        JsonWriter.Write(filledRooms, @"C:\Repos\EasyRooms\EasyRooms.Tests\IntegrationTests\TestData\realFlowRooms.json");
    }

    private void GuardFileName()
    {
        if (string.IsNullOrEmpty(_fileName))
        {
            throw new ArgumentNullException(nameof(_fileName));
        }
    }

    private void Validate(IEnumerable<Room> filledRooms)
        => _ = _validator.IsValid(filledRooms, _persistenceService.SavedOptions.Rooms.ToRoomNames())
            ? default(object)
            : throw new RoomsValidationException();
}