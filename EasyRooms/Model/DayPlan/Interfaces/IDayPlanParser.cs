using EasyRooms.Model.DayPlan.Models;

namespace EasyRooms.Model.DayPlan.Interfaces;

public interface IDayPlanParser
{
    ParsedPlan ParseRows(string path);
}