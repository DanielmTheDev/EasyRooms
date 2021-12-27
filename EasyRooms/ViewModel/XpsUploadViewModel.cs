using System;
using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.DayPlan;
using EasyRooms.Model.DayPlan.Interfaces;
using EasyRooms.Model.FileDialog;
using EasyRooms.Model.FileDialog.Interfaces;
using EasyRooms.Model.Json;
using EasyRooms.Model.Pdf;
using EasyRooms.Model.Pdf.Interfaces;
using EasyRooms.Model.Rooms;
using EasyRooms.Model.Rooms.Interfaces;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Validation;
using EasyRooms.Model.Validation.Exceptions;
using EasyRooms.Model.Validation.Interfaces;
using EasyRooms.ViewModel.Commands;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace EasyRooms.ViewModel;

public class XpsUploadViewModel : BindableBase
{
    public RoomNames Rooms { get; set; }
    public RelayCommand CalculateOccupationsCommand { get; set; }
    public RelayCommand ChooseFileCommand { get; set;}

    private readonly IDayPlanParser _dayPlanParser;
    private readonly IFileDialogOpener _fileDialogOpener;
    private readonly IRoomOccupationsFiller _occupationsFiller;
    private readonly IRoomsValidator _validator;
    private readonly IPdfWriter _pdfWriter;

    private string? _fileName;
    private readonly int _buffer = 1;

    public XpsUploadViewModel(
        IRoomOccupationsFiller occupationsFiller,
        IDayPlanParser dayPlanParser,
        IFileDialogOpener fileDialogOpener,
        IRoomsValidator validator,
        IPdfWriter pdfWriter)
    {
        CalculateOccupationsCommand = new RelayCommand(CalculateOccupations, CanCalculateOccupations);
        ChooseFileCommand = new RelayCommand(OpenFileDialog);
        Rooms = new RoomNames();
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
        var filledRooms = _occupationsFiller.FillRoomOccupations(rows, Rooms, _buffer).ToList();
        Validate(filledRooms);
        _pdfWriter.Write(filledRooms);
        JsonWriter.WriteJson(filledRooms);
    }

    private void Validate(IEnumerable<Room> filledRooms)
        => _ = _validator.IsValid(filledRooms, Rooms)
            ? default(object)
            : throw new RoomsValidationException();
}