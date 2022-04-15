namespace EasyRooms.Model.Pdf.Models;

public record TherapyPlan(
    string Therapist,
    IList<TherapyPlanRow> Rows
);