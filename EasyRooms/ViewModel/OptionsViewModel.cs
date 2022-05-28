using EasyRooms.Model.Persistence.Interfaces;
using EasyRooms.ViewModel.Commands;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace EasyRooms.ViewModel;

public class OptionsViewModel : BindableBase
{
    private readonly IPersistenceService _persistenceService;

    public string RoomsString
    {
        get => _persistenceService.SavedOptions.Rooms.Rooms;
        set => _persistenceService.SavedOptions.Rooms.Rooms = value;
    }
    public string PartnerRoomsString
    {
        get => _persistenceService.SavedOptions.Rooms.PartnerRooms;
        set => _persistenceService.SavedOptions.Rooms.PartnerRooms = value;
    }
    public string MassagesForSpecificRooms
    {
        get => _persistenceService.SavedOptions.Rooms.MassagesForSpecificRooms;
        set => _persistenceService.SavedOptions.Rooms.MassagesForSpecificRooms = value;
    }
    public string Buffer
    {
        get => _persistenceService.SavedOptions.Buffer.ToString();
        set => _persistenceService.SavedOptions.Buffer = int.Parse(value);
    }
    public RelayCommand SaveRoomsCommand { get; }

    public OptionsViewModel(IPersistenceService persistenceService)
    {
        _persistenceService = persistenceService;
        SaveRoomsCommand = new RelayCommand(SaveRooms);
    }

    private void SaveRooms()
        => _persistenceService.SaveOptions();
}