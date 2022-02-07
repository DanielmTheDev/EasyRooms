using EasyRooms.Model.Pdf.Interfaces;
using EasyRooms.Model.Pdf.Models;

namespace EasyRooms.Model.Pdf.Implementations;

public class TherapyPlanCreator : ITherapyPlanCreator
{
    public TherapyPlan Create(IEnumerable<Room> rooms)
    {
        var rows = ExtractTherapyRows(rooms);
        return new TherapyPlan(rows);
    }

    private static IEnumerable<TherapyPlanRow> ExtractTherapyRows(IEnumerable<Room> rooms)
        => rooms.SelectMany(room => room.Occupations
                .Select(occupation => new TherapyPlanRow(
                    occupation.StartTime.ToString(),
                    (occupation.EndTime - occupation.StartTime).ToString(),
                    occupation.TherapyShort,
                    occupation.TherapyLong,
                    occupation.Patient,
                    room.Name,
                    occupation.Therapist)))
            .OrderBy(row => row.Therapist)
            .ThenBy(row => row.StartTime);
}