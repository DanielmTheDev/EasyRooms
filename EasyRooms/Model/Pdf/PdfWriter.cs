using System.Collections.Generic;
using System.IO;
using EasyRooms.Model.Pdf.Models;
using EasyRooms.Model.Rooms.Models;

namespace EasyRooms.Model.Pdf
{
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
            File.WriteAllBytes(@"C:\Users\MuckelbauerD\Downloads\test.pdf", pdfBuilderAggregate.Builder.Build());
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
    }
}