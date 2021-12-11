using System.Collections.Generic;
using EasyRooms.Model.Pdf.Models;
using EasyRooms.Model.Rooms.Models;

namespace EasyRooms.Model.Pdf;

public interface ITherapyPlanCreator
{
    TherapyPlan Create(IEnumerable<Room> rooms);
}