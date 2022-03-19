namespace EasyRooms.Model.Pdf.Models;

public record TherapyPlan(
    string Therapist,
    IEnumerable<TherapyPlanRow> Rows
);