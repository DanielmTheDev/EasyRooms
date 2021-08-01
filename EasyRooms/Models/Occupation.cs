using System;

namespace EasyRooms.Models
{
    public class Occupation
    {
        public string Therapist { get; set; }
        public string TherapyShort { get; set; }
        public string TherapyLong { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
    }
}