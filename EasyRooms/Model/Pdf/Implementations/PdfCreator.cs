using EasyRooms.Model.CommonExtensions;
using EasyRooms.Model.Pdf.Interfaces;
using EasyRooms.Model.Pdf.Models;

namespace EasyRooms.Model.Pdf.Implementations;

public class PdfCreator : IPdfCreator
{
    private readonly IPlansCreator _plansCreator;
    private readonly IPlanPrinter _planPrinter;

    private const double PageHeaderOffset = 40d;
    private const double ColumnsHeaderOffset = 60d;
    private const double InitialRowsOffset = 75d;

    public PdfCreator(IPlansCreator plansCreator, IPlanPrinter planPrinter)
    {
        _plansCreator = plansCreator;
        _planPrinter = planPrinter;
    }

    public IEnumerable<PdfData> Create(IEnumerable<Room> rooms)
    {
        var therapyPlans = _plansCreator.Create(rooms);
        return therapyPlans.Select(WritePdf);
    }

    private PdfData WritePdf(TherapyPlan plan)
    {
        var pdf = Pdf.Create(plan.Therapist);
        _planPrinter.PrintPageHeader(pdf, plan.Therapist, PageHeaderOffset);
        _planPrinter.PrintHeaders(pdf, ColumnsHeaderOffset);
        PrintRows(plan, pdf);
        return pdf;
    }

    private static void PrintRows(TherapyPlan plan, PdfData pdf)
    {
        var rowsOffset = InitialRowsOffset;
        plan.Rows.ForEach(row =>
        {
            PrintRow(row, rowsOffset, pdf);
            rowsOffset += TherapyPlanConstants.LineHeight;
        });
    }

    private static void PrintRow(TherapyPlanRow row, double yOffset, PdfData pdfData)
    {
        var rowStrings = new[] {row.StartTime, row.Duration, row.Room, row.TherapyShort, row.TherapyLong, row.Patient, row.Therapist};
        LinePrinter.PrintLine(pdfData, rowStrings, pdfData.Font, yOffset);
    }
}