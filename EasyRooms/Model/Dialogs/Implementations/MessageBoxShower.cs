using System.Windows;
using EasyRooms.Model.Dialogs.Interfaces;

namespace EasyRooms.Model.Dialogs.Implementations;

public class MessageBoxShower : IMessageBoxShower
{
    public void Success()
    {
        const string text = "Die Pläne wurden erfolgreich erstellt.";
        const string caption = "Erfolgreich";
        ShowMessage(text, caption);
    }

    public void UnknownError(Exception exception)
        => ShowMessage(exception.Message, "Unbekannter Fehler");

    public void NoFreeRoomFound()
    {
        const string text = "Es konnte keine Lösung gefunden werden. Versuchen Sie bitte, die Pufferzeit in den Optionen zu verringern.";
        const string caption = "Fehler";
        ShowMessage(text, caption);
    }

    public void ValidationFailed()
    {
        const string text = "Es wurde ein falsches Ergebnis erzielt. Bitte melden Sie den benutzten Gesamtplan an den Hersteller.";
        const string caption = "Fehler";
        ShowMessage(text, caption);
    }

    private static void ShowMessage(string messageBoxText, string caption)
    {
        const MessageBoxButton button = MessageBoxButton.OK;
        const MessageBoxImage icon = MessageBoxImage.None;
        MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
    }
}