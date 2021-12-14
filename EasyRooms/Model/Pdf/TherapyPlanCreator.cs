using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Pdf.Models;
using EasyRooms.Model.Rooms.Models;

namespace EasyRooms.Model.Pdf;

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