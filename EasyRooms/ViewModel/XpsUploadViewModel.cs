using EasyRooms.ViewModel.Commands;
using Microsoft.Win32;
using System;

namespace EasyRooms.ViewModel
{
    public class XpsUploadViewModel
    {
        public string RoomsString { get; private set; }
        public RelayCommand CalculateOccupationsCommand { get; private set; }
        public RelayCommand ChooseFileCommand { get; private set; }

        private string? _fileName;

        public XpsUploadViewModel()
        {
            RoomsString = "Raum1\nRaum2\nRaum3\nRaum4\nRaum5\n";
            CalculateOccupationsCommand = new RelayCommand(CalculateOccupations, CanCalculateOccupations);
            ChooseFileCommand = new RelayCommand(OpenFileDialog);
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
            throw new NotImplementedException();
        }
    }
}
