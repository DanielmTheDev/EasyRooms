using EasyRooms.Model.Pdf.Interfaces;
using EasyRooms.Model.Pdf.Models;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Writer;

namespace EasyRooms.Model.Pdf.Implementations;

public class PdfCreator : IPdfCreator
{
    private readonly IPlansCreator _plansCreator;
    private readonly IHeaderPrinter _headerPrinter;

    public PdfCreator(IPlansCreator plansCreator, IHeaderPrinter headerPrinter)
    {
        _plansCreator = plansCreator;
        _headerPrinter = headerPrinter;
    }

    public IEnumerable<PdfAggregate> Create(IEnumerable<Room> rooms, PdfAggregate pdf)
    {
        var therapyPlans = _plansCreator.Create(rooms);
        return therapyPlans.Select(plan => WritePdf(plan, pdf));
    }

    private PdfAggregate WritePdf(TherapyPlan plan, PdfAggregate pdf)
    {
        var page = pdf.Builder.AddPage(PageSize.A4);
        _headerPrinter.PrintPageHeader(pdf, plan.Therapist, TherapyPlanConstants.PageHeaderOffset, page);
        _headerPrinter.PrintColumnHeaders(pdf, TherapyPlanConstants.ColumnsHeaderOffset, page);
        PrintRows(plan, pdf, page);
        return pdf;
    }

    private static void PrintRows(TherapyPlan plan, PdfAggregate pdf, PdfPageBuilder page)
    {
        var rowsOffset = TherapyPlanConstants.InitialRowsOffset;
        for (var i = 0; i < plan.Rows.Count; i++)
        {
            PrintRow(plan.Rows[i], i, rowsOffset, pdf, page);
            rowsOffset += TherapyPlanConstants.LineHeight;
        }
    }

    private static void PrintRow(TherapyPlanRow row, int rowIndex, double yOffset, PdfAggregate pdfAggregate, PdfPageBuilder page)
    {
        var rowStrings = new[] {row.StartTime, row.Duration, row.Comment, row.Room, row.TherapyShort, row.Patient};
        LinePrinter.PrintLine(rowIndex, rowStrings, pdfAggregate.Font, yOffset, page);
    }
}