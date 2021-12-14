using System.Collections.Generic;
using EasyRooms.Model.Rooms.Models;
using EasyRooms.Model.Rows.Models;

namespace EasyRooms.Model.Therapies.PartnerTherapies;

public interface IPartnerTherapiesAdder
{
    void AddPartnerTherapies(IEnumerable<Room> rooms, ICollection<Row> orderedRows, int bufferInMinutes);
}