namespace EasyRooms.Model.Pdf.Models;

public record TherapyPlanRow(
    string StartTime,
    string Duration,
    string Comment,
    string TherapyShort,
    string Patient,
    string Room,
    string Therapist,
    bool ShouldHighlight
);