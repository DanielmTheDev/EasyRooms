using EasyRooms.Model.TimeWindow.Interfaces;

namespace EasyRooms.Model.TimeWindow.Implementations;

public class TimeWindowValueHolder : ITimeWindowValueHolder
{
    public string StartTime { get; set; }
    public string EndTime { get; set; }

    public TimeWindowValueHolder()
    {
        StartTime = "14:00";
        EndTime = "16:00";
    }
}