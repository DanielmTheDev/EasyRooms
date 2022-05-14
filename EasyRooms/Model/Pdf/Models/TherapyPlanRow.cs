namespace EasyRooms.Model.Pdf.Models;

public record TherapyPlanRow(
    string StartTime,
    string Duration,
    string Comment,
    string TherapyShort,
    string TherapyLong,
    string Patient,
    string Room,
    string Therapist
);