namespace EasyRooms.Model.Dialogs.Interfaces;

public interface IMessageBoxShower
{
    void Success(string text = "Die Pläne wurden erfolgreich erstellt.");
    void Error(string text);
    void UnknownError(Exception exception);
    void NoFreeRoomFound();
    void ValidationFailed();
}