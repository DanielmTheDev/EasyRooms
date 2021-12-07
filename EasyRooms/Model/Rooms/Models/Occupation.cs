using System;
using EasyRooms.Model.Rows.Models;

#nullable disable
namespace EasyRooms.Model.Rooms.Models

{
    public class Occupation
    {
        public string Therapist { get; }
        public string Patient { get; }
        public string TherapyShort { get; }
        public string TherapyLong { get; }
        public TimeSpan StartTime { get; }
        public TimeSpan EndTime { get; }

        // ReSharper disable once UnusedMember.Global
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
