using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Constants;
using EasyRooms.Model.Rooms.Models;

namespace EasyRooms.Model.Validation
{
    public class RoomsValidator
    {
        public bool Validate(IEnumerable<Room> rooms, RoomNames roomNames)
        {
            rooms.All(room => room.Occupations.All(occupation =>
            {
                if(occupation.TherapyShort == CommonConstants.PartnerString)
                    return roomNames.
                
            }));
        }
    }
}