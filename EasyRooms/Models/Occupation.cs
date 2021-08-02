using System;

namespace EasyRooms.Models
{
    public record Occupation(string Therapist, string Patient, string TherapyShort, string TherapyLong, TimeSpan From, TimeSpan To) { }
}