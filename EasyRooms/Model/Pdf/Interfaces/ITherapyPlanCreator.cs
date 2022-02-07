using EasyRooms.Model.Pdf.Models;

namespace EasyRooms.Model.Pdf.Interfaces;

public interface ITherapyPlanCreator
{
    TherapyPlan Create(IEnumerable<Room> rooms);
}