using System;
using EasyRooms.Model.Rows.Models;

namespace EasyRooms.Model.Rooms.Models

{
    public class Occupation
    {
        public string Therapist { get; init; }
        public string Patient { get; init; }
        public string TherapyShort { get; init; }
        public string TherapyLong { get; init; }
        public TimeSpan StartTime { get; init; }
        public TimeSpan EndTime { get; init; }

        public Occupation()
        {
            
        }
        
        public Occupation(Row row, TimeSpan startTime, TimeSpan endTime) : this(row.Therapist,row.Patient, row.TherapyShort, row.TherapyLong, startTime, endTime)
        {
        }

        public Occupation(string therapist, string patient, string therapyShort, string therapyLong, TimeSpan startTime, TimeSpan endTime)
        {
            Therapist = therapist;
            Patient = patient;
            TherapyShort = therapyShort;
            TherapyLong = therapyLong;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
