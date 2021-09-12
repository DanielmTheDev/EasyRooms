using System.Collections.Generic;
using EasyRooms.Model.Models;

namespace EasyRooms.Model.Interfaces;

public interface IDayPlanParser
{
    IEnumerable<Row> ParseDayPlan(string path);
}
