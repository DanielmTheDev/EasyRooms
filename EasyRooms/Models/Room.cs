using System;
using System.Collections.Generic;

namespace EasyRooms.Models
{
    public class Room
    {
        public string Name { get; set; }
        public int Priority { get; set; }
        private IEnumerable<Occupation> Occupations { get; }

        public Room(string name, int priority)
        {
            Name = name;
            Priority = priority;
            Occupations = new List<Occupation>();
        }

        public Room AddOccupation(Occupation occupation)
        {
            throw new NotImplementedException();
        }

        internal bool IsEmptyAt(string startTime, string duration)
        {
            throw new NotImplementedException();
        }
    }
}
