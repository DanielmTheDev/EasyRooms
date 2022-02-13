using EasyRooms.Model.Buffer.Implementations;
using EasyRooms.Model.Rooms.Interfaces;
using EasyRooms.Model.TimeWindow.Interfaces;
using EasyRooms.ViewModel.Commands;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace EasyRooms.ViewModel;

public class OptionsViewModel : BindableBase
{
    private readonly IRoomNamesService _roomNamesService;
    private readonly ITimeWindowService _timesWindowService;
    private readonly IBufferService _bufferService;

    public string RoomsString
    {
        get => _roomNamesService.Rooms.RoomsString;
        set => _roomNamesService.Rooms.RoomsString = value;
    }
    public string PartnerRoomsString
    {
        get => _roomNamesService.Rooms.PartnerRoomsString;
        set => _roomNamesService.Rooms.PartnerRoomsString = value;
    }
    public string RoomsForSpecificMassages
    {
        get => _roomNamesService.Rooms.RoomsForSpecificMassages;
        set => _roomNamesService.Rooms.RoomsForSpecificMassages = value;
    }
    public string MassagesForSpecificRooms
    {
        get => _roomNamesService.Rooms.MassagesForSpecificRooms;
        set => _roomNamesService.Rooms.MassagesForSpecificRooms = value;
    }
    public string StartTime
    {
        get => _timesWindowService.StartTime;
        set => _timesWindowService.StartTime = value;
    }
    public string EndTime
    {
        get => _timesWindowService.EndTime;
        set => _timesWindowService.EndTime = value;
    }
    public string Buffer
    {
        get => _bufferService.Buffer;
        set => _bufferService.Buffer = value;
    }

    public RelayCommand SaveRoomsCommand { get; }

    public OptionsViewModel(IRoomNamesService roomNamesService, ITimeWindowService timesWindowService, IBufferService bufferService)
    {
        _roomNamesService = roomNamesService;
        _timesWindowService = timesWindowService;
        _bufferService = bufferService;
        SaveRoomsCommand = new RelayCommand(SaveRooms);
    }

    private void SaveRooms()
        => _roomNamesService.SaveRooms();
}