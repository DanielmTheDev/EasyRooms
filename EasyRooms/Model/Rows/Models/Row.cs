namespace EasyRooms.Model.Rows.Models;

public class Row
{
    public string StartTime { get; }
    public int Duration { get; }
    public string TherapyShort { get; }
    public string TherapyLong { get; }
    public string Patient { get; }
    public string Therapist { get; }
    public string Comment { get; set; }

    public Row(string startTime,
        int duration,
        string therapyShort,
        string therapyLong,
        string patient,
        string therapist,
        string comment)
    {
        StartTime = startTime;
        Duration = duration;
        TherapyShort = therapyShort;
        TherapyLong = therapyLong;
        Patient = patient;
        Therapist = therapist;
        Comment = comment;
    }

    public string EndTime => TimeOnly.Parse(StartTime).AddMinutes(Duration).ToString();
    public TimeSpan StartTimeAsTimeSpan => TimeSpan.Parse(StartTime);
    public TimeSpan EndTimeAsTimeSpan => TimeSpan.Parse(EndTime);
    public TimeSpan DurationAsTimeSpan => TimeSpan.FromMinutes(Duration);
}