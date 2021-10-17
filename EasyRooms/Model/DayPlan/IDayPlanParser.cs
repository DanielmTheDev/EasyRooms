using System.Collections.Generic;
using EasyRooms.Model.Rows.Models;

namespace EasyRooms.Model.DayPlan
{
    public interface IDayPlanParser
    {
        IEnumerable<Row> ParseDayPlan(string path);
    }
    
}