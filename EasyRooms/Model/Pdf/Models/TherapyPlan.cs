using System.Collections.Generic;

namespace EasyRooms.Model.Pdf.Models
{
    public record TherapyPlan(
        IEnumerable<TherapyPlanRow> Rows
    );
}