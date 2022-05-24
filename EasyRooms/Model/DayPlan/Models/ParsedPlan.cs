namespace EasyRooms.Model.DayPlan.Models;

public record ParsedPlan(DateOnly Date, IEnumerable<Row> Rows);