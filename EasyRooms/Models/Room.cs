using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyRooms.Models
{
    public record Room
    {
        public string Name { get; set; }
        public int Priority { get; set; }
        private List<Occupation> Occupations { get; }

        public Room(string name, int priority)
        {
            Name = name;
            Priority = priority;
            Occupations = new List<Occupation>();
        }

        public Room AddOccupation(Occupation occupation)
        {
            Occupations.Add(occupation);
            return this;
        }

        public bool IsOccupiedAt(TimeSpan startTime, TimeSpan endTime) 
            => Occupations.Any(occupation => startTime < occupation.StartTime && endTime < occupation.EndTime);
    }
}
