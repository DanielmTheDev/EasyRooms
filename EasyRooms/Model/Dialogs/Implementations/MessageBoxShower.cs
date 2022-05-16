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

    public void ShowNoFreeRoomFoundMessage()
    {
        const string messageBoxText = "Es konnte keine Lösung gefunden werden. Versuchen Sie bitte, die Pufferzeit in den Optionen zu verringern.";
        const string caption = "Fehler";
        const MessageBoxButton button = MessageBoxButton.OK;
        const MessageBoxImage icon = MessageBoxImage.None;
        MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
    }
}