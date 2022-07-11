using System.Windows;
using EasyRooms.Model.Dialogs.Interfaces;

namespace EasyRooms.Model.Dialogs.Implementations;

public class MessageBoxShower : IMessageBoxShower
{
    public void Success(string text)
        => ShowMessage(text, "Erfolgreich");

    public void Error(string text)
        => ShowErrorMessage(text, "Fehler");

    public void UnknownError(Exception exception)
        => ShowErrorMessage(exception.Message, "Unbekannter Fehler.");

    public void NoFreeRoomFound()
    {
        const string text = "Es konnte keine Lösung gefunden werden. Versuchen Sie bitte, die Pufferzeit in den Optionen zu verringern.";
        const string caption = "Fehler";
        ShowErrorMessage(text, caption);
    }

    public void ValidationFailed()
    {
        const string text = "Es wurde ein falsches Ergebnis erzielt. Bitte melden Sie den benutzten Gesamtplan an den Hersteller.";
        const string caption = "Fehler";
        ShowErrorMessage(text, caption);
    }

    private static void ShowMessage(string text, string caption)
    {
        const MessageBoxButton button = MessageBoxButton.OK;
        const MessageBoxImage icon = MessageBoxImage.None;
        MessageBox.Show(text, caption, button, icon, MessageBoxResult.Yes);
    }

    private static void ShowErrorMessage(string text, string caption)
    {
        const MessageBoxButton button = MessageBoxButton.OK;
        const MessageBoxImage icon = MessageBoxImage.Error;
        MessageBox.Show(text, caption, button, icon, MessageBoxResult.Yes);
    }
}