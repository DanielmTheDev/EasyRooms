using EasyRooms.Model.Pdf.Models;

namespace EasyRooms.Model.Pdf.Interfaces;

public interface IPlansCreator
{
    IEnumerable<TherapyPlan> Create(IEnumerable<Room> rooms);
}