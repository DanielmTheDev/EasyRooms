using EasyRooms.Model.Pdf.Interfaces;
using EasyRooms.Model.Pdf.Models;

namespace EasyRooms.Model.Pdf.Implementations;

public class TherapyPlanRowsPrinter : ITherapyPlanRowsPrinter
{
    private readonly ITherapyPlanCreator _therapyPlanCreator;

    public TherapyPlanRowsPrinter(ITherapyPlanCreator therapyPlanCreator)
        => _therapyPlanCreator = therapyPlanCreator;

    public void PrintRows(IEnumerable<Room> rooms, PdfBuilderAggregate pdfBuilderAggregate, double yOffset)
    {
        var rows = _therapyPlanCreator.Create(rooms).Rows.ToList();
        for (var i = 0; i < rows.Count; i++)
        {
            yOffset = AdjustYOffsetIfTherapistChanged(yOffset, i, rows);
            PrintRow(rows[i], yOffset, pdfBuilderAggregate);
            yOffset += TherapyPlanConstants.LineHeight;
        }
    }

    private static double AdjustYOffsetIfTherapistChanged(double yOffset, int i, IReadOnlyList<TherapyPlanRow> rows)
    {
        var isSubsequentTherapist = i > 0 && rows[i].Therapist != rows[i - 1].Therapist;
        return isSubsequentTherapist
            ? yOffset + TherapyPlanConstants.LineHeight
            : yOffset;
    }

    private static void PrintRow(TherapyPlanRow row, double yOffset, PdfBuilderAggregate pdfBuilderAggregate)
    {
        var rowStrings = new[] { row.StartTime, row.Duration, row.Room, row.TherapyShort, row.TherapyLong, row.Patient, row.Therapist };
        LinePrinter.PrintLine(pdfBuilderAggregate, rowStrings, pdfBuilderAggregate.Font, yOffset);
    }
}