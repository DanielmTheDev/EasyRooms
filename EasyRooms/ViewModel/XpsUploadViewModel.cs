using EasyRooms.Model.DayPlan.Interfaces;
using EasyRooms.Model.DayPlan.Models;
using EasyRooms.Model.Dialogs.Interfaces;
using EasyRooms.Model.FileDialog.Interfaces;
using EasyRooms.Model.Pdf.Interfaces;
using EasyRooms.Model.Persistence.Extensions;
using EasyRooms.Model.Persistence.Interfaces;
using EasyRooms.Model.Rooms.Exceptions;
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
    private readonly IMessageBoxShower _messageBoxShower;
    private string? _fileName;

    public XpsUploadViewModel(
        IRoomOccupationsFiller occupationsFiller,
        IDayPlanParser dayPlanParser,
        IFileDialogOpener fileDialogOpener,
        IRoomsValidator validator,
        IPdfWriter pdfWriter,
        IPersistenceService persistenceService,
        IMessageBoxShower messageBoxShower)
    {
        CalculateOccupationsCommand = new(CalculateOccupations, CanCalculateOccupations);
        ChooseFileCommand = new(OpenFileDialog);
        _persistenceService = persistenceService;
        _messageBoxShower = messageBoxShower;
        _occupationsFiller = occupationsFiller;
        _dayPlanParser = dayPlanParser;
        _fileDialogOpener = fileDialogOpener;
        _validator = validator;
        _pdfWriter = pdfWriter;
        _fileName = "c:\\Users\\MuckelbauerD\\Downloads\\Test 2. 27.05.xps";
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
        var parsedPlan = _dayPlanParser.ParseRows(_fileName!);
        var roomNames = _persistenceService.SavedOptions.Rooms.ToRoomNames();
        var savedOptionsBuffer = _persistenceService.SavedOptions.Buffer;
        try
        {
            CreateResultPdf(parsedPlan, roomNames, savedOptionsBuffer);
            _messageBoxShower.Success();
        }
        catch (NoFreeRoomException)
        {
            _messageBoxShower.NoFreeRoomFound();
        }
        catch (RoomsValidationException)
        {
            _messageBoxShower.ValidationFailed();
        }
    }

    private void CreateResultPdf(ParsedPlan plan, RoomNames roomNames, int savedOptionsBuffer)
    {
        var filledRooms = _occupationsFiller
            .FillRoomOccupations(plan.Rows, roomNames, savedOptionsBuffer)
            .ToList();
        Validate(filledRooms);
        _pdfWriter.Write(filledRooms, plan.Date);
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