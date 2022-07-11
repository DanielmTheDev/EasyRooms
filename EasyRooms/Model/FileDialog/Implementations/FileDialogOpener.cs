using EasyRooms.Model.FileDialog.Interfaces;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace EasyRooms.Model.FileDialog.Implementations;

public class FileDialogOpener : IFileDialogOpener
{
    public string GetFileName()
    {
        var dialog = new OpenFileDialog
        {
            DefaultExt = ".xps",
            Filter = "XPS Files (*.xps)|*.xps"
        };

        var result = dialog.ShowDialog();

        return result == true
            ? dialog.FileName
            : string.Empty;
    }

    public string GetDirectory()
    {
        var dialog = new CommonOpenFileDialog();
        dialog.IsFolderPicker = true;
        var result = dialog.ShowDialog();
        return result == CommonFileDialogResult.Ok
            ? dialog.FileName
            : string.Empty;
    }
}