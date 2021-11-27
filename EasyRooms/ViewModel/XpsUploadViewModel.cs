using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EasyRooms.Model.DayPlan;
using EasyRooms.Model.FileDialog;
using EasyRooms.Model.Rooms;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Validation;
using EasyRooms.Model.Validation.Exceptions;
using EasyRooms.ViewModel.Commands;
using Newtonsoft.Json;

namespace EasyRooms.ViewModel

{
    public class XpsUploadViewModel : BindableBase
    {
        public RoomNames Rooms { get; set; }
        public RelayCommand CalculateOccupationsCommand { get; private set; }
        public RelayCommand ChooseFileCommand { get; private set; }

        private readonly IDayPlanParser _dayPlanParser;
        private readonly IFileDialogOpener _fileDialogOpener;
        private readonly IRoomOccupationsFiller _occupationsFiller;
        private readonly IRoomsValidator _validator;

        private string? _fileName;
        private readonly int _buffer = 1;

        public XpsUploadViewModel(
            IRoomOccupationsFiller occupationsFiller,
            IDayPlanParser dayPlanParser,
            IFileDialogOpener fileDialogOpener,
            IRoomsValidator validator)
        {
            HookUpCommands();
            Rooms = new RoomNames();
            _occupationsFiller = occupationsFiller;
            _dayPlanParser = dayPlanParser;
            _fileDialogOpener = fileDialogOpener;
            _validator = validator;
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
            var filledRooms = _occupationsFiller.FillRoomOccupations(rows, Rooms, _buffer).ToList();
            _ = _validator.IsValid(filledRooms, Rooms) ? default(object) : throw new RoomsValidationException();
            WriteJson(filledRooms);
        }

        private static void WriteJson(IEnumerable<Room>? filledRooms)
        {
            var serializedRooms = JsonConvert.SerializeObject(filledRooms, Formatting.Indented);
            File.WriteAllText(@"C:\Repos\EasyRooms\EasyRooms.Tests\IntegrationTests\TestData\realFlowRooms.json", serializedRooms);
        }
    }
}