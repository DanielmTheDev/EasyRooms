using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using EasyRooms.Model.Pdf.Models;
using EasyRooms.Model.Rooms.Models;

namespace EasyRooms.Model.Pdf;

public class PdfWriter : IPdfWriter
{
    private readonly ITherapyPlanRowsPrinter _therapyPlanRowsPrinter;
    private readonly ITherapyPlanHeadersPrinter _therapyPlanHeadersPrinter;

    public PdfWriter(ITherapyPlanCreator therapyPlanCreator, ITherapyPlanHeadersPrinter therapyPlanHeadersPrinter)
    {
        _therapyPlanHeadersPrinter = therapyPlanHeadersPrinter;
        _therapyPlanRowsPrinter = new TherapyPlanRowsPrinter(therapyPlanCreator);
    }

    public void Write(IEnumerable<Room> rooms)
    {
        var pdfBuilderAggregate = PdfBuilderAggregateCreator.Create();
        PrintHeaders(pdfBuilderAggregate);
        PrintRows(pdfBuilderAggregate, rooms);
        const string path = @"C:\Users\MuckelbauerD\Downloads\test.pdf";
        File.WriteAllBytes(path, pdfBuilderAggregate.Builder.Build());
        OpenPdf(path);
    }

    private void PrintHeaders(PdfBuilderAggregate pdfBuilderAggregate)
    {
        const double headersYOffset = 10d;
        _therapyPlanHeadersPrinter.PrintHeaders(pdfBuilderAggregate, headersYOffset);
    }

    private void PrintRows(PdfBuilderAggregate pdfBuilderAggregate, IEnumerable<Room> rooms)
    {
        const double rowsYOffset = 25d;
        _therapyPlanRowsPrinter.PrintRows(rooms, pdfBuilderAggregate, rowsYOffset);
    }

    private static void OpenPdf(string filePath)
    {
        var process = new Process();
        process.StartInfo.FileName = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
        process.StartInfo.Arguments = filePath;
        process.Start();
    }
}