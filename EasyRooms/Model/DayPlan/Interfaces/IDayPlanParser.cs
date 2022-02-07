namespace EasyRooms.Model.DayPlan.Interfaces;

public interface IDayPlanParser
{
    IEnumerable<Row> ParseDayPlan(string path);
}