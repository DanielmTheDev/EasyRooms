using EasyRooms.Model.Persistence.Models;

namespace EasyRooms.Model.Persistence.Interfaces;

public interface IOptionsPersister
{
    public void SaveOptions();
    public SavedOptions SavedOptions { get; }
}