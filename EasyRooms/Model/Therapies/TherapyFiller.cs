using System.Collections.Generic;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;
using EasyRooms.Model.Therapies.NormalTherapies;
using EasyRooms.Model.Therapies.PartnerTherapies;
using EasyRooms.Model.Therapies.Preparations;
using EasyRooms.Model.Therapies.RoomSpecificTherapies;

namespace EasyRooms.Model.Therapies;

public class TherapyFiller : ITherapyFiller
{
    private readonly IPartnerTherapiesAdder _partnerTherapiesAdder;
    private readonly IRoomSpecificMassagesAdder _roomSpecificMassagesAdder;
    private readonly INormalTherapiesAdder _normalTherapiesAdder;
    private readonly IPreparationsAdder _preparationsAdder;

    public TherapyFiller(
        IPartnerTherapiesAdder partnerTherapiesAdder,
        IRoomSpecificMassagesAdder roomSpecificMassagesAdder,
        INormalTherapiesAdder normalTherapiesAdder,
        IPreparationsAdder preparationsAdder)
    {
        _partnerTherapiesAdder = partnerTherapiesAdder;
        _roomSpecificMassagesAdder = roomSpecificMassagesAdder;
        _normalTherapiesAdder = normalTherapiesAdder;
        _preparationsAdder = preparationsAdder;
    }

    public void AddAllTherapies(List<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames)
    {
        _preparationsAdder.AddPreparations(rooms, orderedRows);
        _partnerTherapiesAdder.AddPartnerTherapies(rooms, orderedRows, bufferInMinutes);
        _roomSpecificMassagesAdder.AddRoomSpecificMassages(rooms, orderedRows, bufferInMinutes, roomNames);
        _normalTherapiesAdder.AddNormalTherapies(rooms, orderedRows, bufferInMinutes);
    }
}