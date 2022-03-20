using EasyRooms.Model.Persistence.Models;

namespace EasyRooms.Model.Persistence.Interfaces;

public interface IPersistenceService
{
    public void SaveOptions();
    public SavedOptions SavedOptions { get; set; }
}