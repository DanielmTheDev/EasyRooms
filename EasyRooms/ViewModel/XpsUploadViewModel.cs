﻿using Microsoft.Win32;
using EasyRooms.Interfaces;
using EasyRooms.ViewModel.Commands;


namespace EasyRooms.ViewModel
{
    public class XpsUploadViewModel
    {
        public string RoomsString { get; set; }
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
            var rows = _dayPlanParser.ParseDayPlan(_fileName);
            var rooms = RoomsString.Split('\n');
            var occupations = _occupationsFiller.FillRoomOccupations(rows, rooms, _buffer);
        }
    }
}