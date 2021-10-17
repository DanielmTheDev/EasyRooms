using System.IO;
using Microsoft.Win32;

namespace EasyRooms.Model.FileDialog
{
    public class FileDialogOpener : IFileDialogOpener
    {
        public string GetFileNameFromDialog()
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = ".xps",
                Filter = "XPS Files (*.xps)|*.xps"
            };

            var result = dialog.ShowDialog();

            return result == true
                ? dialog.FileName
                : throw new FileNotFoundException();
        }
    }
    
}