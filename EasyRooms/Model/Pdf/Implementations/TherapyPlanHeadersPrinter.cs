using EasyRooms.Model.Pdf.Interfaces;
using EasyRooms.Model.Pdf.Models;

namespace EasyRooms.Model.Pdf.Implementations;

public class TherapyPlanHeadersPrinter : ITherapyPlanHeadersPrinter
{
    public void PrintHeaders(PdfBuilderAggregate pdfBuilderAggregate, double yOffset)
    {
        var headerStrings = new [] { "Beginn", "Dauer", "Raum", "Behandlung", string.Empty, "Patient", "Personal" };
        LinePrinter.PrintLine(pdfBuilderAggregate, headerStrings, pdfBuilderAggregate.BoldFont, yOffset);
    }
}