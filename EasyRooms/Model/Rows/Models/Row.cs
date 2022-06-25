namespace EasyRooms.Model.Rows.Models;

public record Row(
    string StartTime,
    int Duration,
    string TherapyShort,
    string TherapyLong,
    string Patient,
    string Therapist,
    string Comment)
{
    public string EndTime => TimeOnly.Parse(StartTime).AddMinutes(Duration).ToString();
    public TimeSpan StartTimeAsTimeSpan => TimeSpan.Parse(StartTime);
    public TimeSpan EndTimeAsTimeSpan => TimeSpan.Parse(EndTime);
    public TimeSpan DurationAsTimeSpan => TimeSpan.FromMinutes(Duration);
}