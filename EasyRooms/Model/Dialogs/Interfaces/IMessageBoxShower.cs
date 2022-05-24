namespace EasyRooms.Model.Dialogs.Interfaces;

public interface IMessageBoxShower
{
    void Success();
    void NoFreeRoomFound();
    void ValidationFailed();
}