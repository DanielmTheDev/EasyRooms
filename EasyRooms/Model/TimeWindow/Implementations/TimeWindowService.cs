using EasyRooms.Model.TimeWindow.Interfaces;

namespace EasyRooms.Model.TimeWindow.Implementations;

public class TimeWindowService : ITimeWindowService
{
    public string StartTime { get; set; }
    public string EndTime { get; set; }

    public TimeWindowService()
    {
        StartTime = "14:00";
        EndTime = "16:00";
    }
}