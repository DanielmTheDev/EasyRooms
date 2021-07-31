using System.Collections.Generic;

namespace EasyRooms.Interfaces
{
    public interface IDayPlanParser
    {
        IEnumerable<Row> ParseDayPlan(string path);
    }
}
