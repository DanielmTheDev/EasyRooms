using System.Collections.Generic;
using EasyRooms.Model.Pdf.Models;
using EasyRooms.Model.Rooms.Models;

namespace EasyRooms.Model.Pdf
{
    public class TherapyPlanRowsPrinter : ITherapyPlanRowsPrinter
    {
        private readonly ITherapyPlanCreator _therapyPlanCreator;

        public TherapyPlanRowsPrinter(ITherapyPlanCreator therapyPlanCreator) 
            => _therapyPlanCreator = therapyPlanCreator;

        public void PrintRows(IEnumerable<Room> rooms, PdfBuilderAggregate pdfBuilderAggregate, double yOffset)
        {
            foreach (var row in _therapyPlanCreator.Create(rooms).Rows)
            {
                PrintRow(row, yOffset, pdfBuilderAggregate);
                yOffset += TherapyPlanConstants.LineHeight;
            }
        }

        private static void PrintRow(TherapyPlanRow row, double yOffset, PdfBuilderAggregate pdfBuilderAggregate)
        {
            var rowStrings = new[] { row.StartTime, row.Duration, row.Room, row.TherapyShort, row.TherapyLong, row.Patient, row.Therapist };
            LinePrinter.PrintLine(pdfBuilderAggregate, rowStrings, pdfBuilderAggregate.Font, yOffset);
        }
    }
}