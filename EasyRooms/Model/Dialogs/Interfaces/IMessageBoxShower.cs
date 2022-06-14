namespace EasyRooms.Model.Dialogs.Interfaces;

public interface IMessageBoxShower
{
    void Success();
    void UnknownError(Exception exception);
    void NoFreeRoomFound();
    void ValidationFailed();
}