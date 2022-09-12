using EasyRooms.Model.Persistence.Interfaces;
using EasyRooms.ViewModel.Commands;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace EasyRooms.ViewModel;

public class OptionsViewModel : BindableBase
{
    private readonly IOptionsPersister _optionsPersister;

    public string RoomsString
    {
        get => _optionsPersister.SavedOptions.Rooms.Rooms;
        set => _optionsPersister.SavedOptions.Rooms.Rooms = value;
    }
    public string PartnerRoomsString
    {
        get => _optionsPersister.SavedOptions.Rooms.PartnerRooms;
        set => _optionsPersister.SavedOptions.Rooms.PartnerRooms = value;
    }
    public string PartnerTherapyName
    {
        get => _optionsPersister.SavedOptions.Rooms.PartnerTherapyName;
        set => _optionsPersister.SavedOptions.Rooms.PartnerTherapyName = value;
    }
    public string MassagesForSpecificRooms
    {
        get => _optionsPersister.SavedOptions.Rooms.MassagesForSpecificRooms;
        set => _optionsPersister.SavedOptions.Rooms.MassagesForSpecificRooms = value;
    }
    public string Buffer
    {
        get => _optionsPersister.SavedOptions.Buffer.ToString();
        set => _optionsPersister.SavedOptions.Buffer = int.Parse(value);
    }
    public RelayCommand SaveRoomsCommand { get; }

    public OptionsViewModel(IOptionsPersister optionsPersister)
    {
        _optionsPersister = optionsPersister;
        SaveRoomsCommand = new(SaveRooms);
    }

    private void SaveRooms()
        => _optionsPersister.SaveOptions();
}