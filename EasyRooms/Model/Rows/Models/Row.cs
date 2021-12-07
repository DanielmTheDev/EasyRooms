namespace EasyRooms.Model.Rows.Models
{
    //todo either have start and end here as fields or  duration in occupations
    public record Row(string StartTime, string Duration, string TherapyShort, string TherapyLong, string Patient, string Therapist);
}