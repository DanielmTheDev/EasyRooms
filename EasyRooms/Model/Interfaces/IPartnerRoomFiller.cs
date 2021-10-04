using System.Collections.Generic;
using EasyRooms.Model.Models;

namespace EasyRooms.Model.Interfaces;

public interface IPartnerRoomFiller
{
    void AddPartnerTherapies(List<Room> rooms, List<Row> orderedRows, int bufferInMinutes);
}