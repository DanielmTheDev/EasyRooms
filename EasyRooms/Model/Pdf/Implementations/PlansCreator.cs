using EasyRooms.Model.Pdf.Interfaces;
using EasyRooms.Model.Pdf.Models;

namespace EasyRooms.Model.Pdf.Implementations;

public class PlansCreator : IPlansCreator
{
    public IEnumerable<TherapyPlan> Create(IEnumerable<Room> rooms)
    {
        var groupedRows = ExtractTherapyRows(rooms)
            .GroupBy(row => row.Therapist);

        return groupedRows.Select(grouping => new TherapyPlan(grouping.Key, grouping.ToList()));
    }

    private static IEnumerable<TherapyPlanRow> ExtractTherapyRows(IEnumerable<Room> rooms)
        => rooms.SelectMany(room => room.Occupations
                .Select(occupation => new TherapyPlanRow(
                    occupation.StartTime.ToString("hh\\:mm"),
                    (occupation.EndTime - occupation.StartTime).ToString("hh\\:mm"),
                    occupation.Comment,
                    occupation.TherapyShort,
                    occupation.Patient,
                    occupation.TouchesAdjacent ? $"* {room.Name}" : room.Name,
                    occupation.Therapist)))
            .OrderBy(row => row.Therapist)
            .ThenBy(row => row.StartTime);
}