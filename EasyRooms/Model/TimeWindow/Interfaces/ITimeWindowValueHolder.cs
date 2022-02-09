namespace EasyRooms.Model.TimeWindow.Interfaces;

public interface ITimeWindowValueHolder
{
    string StartTime { get; set; }
    string EndTime { get; set; }
    TimeOnly StartTimeAsTimeOnly => TimeOnly.Parse(StartTime);
    TimeOnly EndTimeAsTimeOnly => TimeOnly.Parse(EndTime);
}