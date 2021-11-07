using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyRooms.Model.Rooms.Models

{
    public record Room(string Name, int Priority)
    {
        public bool IsPartnerRoom { get; set; }
        public bool IsMassageSpecificRoom { get; set; }

        public IList<Occupation> Occupations { get; } = new List<Occupation>();

        public Room AddOccupation(Occupation occupation)
        {
            Occupations.Add(occupation);
            return this;
        }

        public bool IsOccupiedAt(TimeSpan startTime, TimeSpan endTime, int bufferInMinutes)
            => Occupations.Any(occupation =>
                startTime < GetEndTimeWithBuffer(occupation.EndTime, bufferInMinutes)
                && endTime > occupation.StartTime);

        private static TimeSpan GetEndTimeWithBuffer(TimeSpan endTime, int bufferInMinutes)
        {
            var minutes = TimeSpan.FromMinutes(bufferInMinutes);
            return endTime.Add(minutes);
        }
    }
}