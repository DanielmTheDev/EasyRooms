using System.Collections.Generic;

namespace EasyRooms.Models
{
    public class Room
    {
        public string Name { get; set; }

        public IEnumerable<Occupation> Occupations { get; set; }

        public Room(string name)
        {
            Name = name;
            Occupations = new List<Occupation>();
        }
    }
}
