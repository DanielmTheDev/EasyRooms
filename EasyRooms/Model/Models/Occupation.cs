using System;

namespace EasyRooms.Model.Models;

public record Occupation(string Therapist, string Patient, string TherapyShort, string TherapyLong, TimeSpan StartTime, TimeSpan EndTime);
