using EasyRooms.Model.Pdf.Models;

namespace EasyRooms.Model.Pdf
{
    public class TherapyPlanHeadersPrinter : ITherapyPlanHeadersPrinter
    {
        public void PrintHeaders(PdfBuilderAggregate pdfBuilderAggregate, double yOffset)
        {
            var headerStrings = new [] { "Beginn", "Dauer", "Behandlung", string.Empty, "Patient", "Raum", "Personal" };
            LinePrinter.PrintLine(pdfBuilderAggregate, headerStrings, pdfBuilderAggregate.BoldFont, yOffset);
        }
    }
}