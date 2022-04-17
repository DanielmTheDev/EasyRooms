using System.Windows;
using EasyRooms.Model.Dialogs.Interfaces;

namespace EasyRooms.Model.Dialogs.Implementations;

public class MessageBoxShower : IMessageBoxShower
{
    public void ShowSuccessMessage()
    {
        const string messageBoxText = "Die Pläne wurden erfolgreich erstellt.";
        const string caption = "Erfolgreich";
        const MessageBoxButton button = MessageBoxButton.OK;
        const MessageBoxImage icon = MessageBoxImage.None;
        MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
    }
}