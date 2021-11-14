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
            return rooms.All(room => room.Occupations.All(occupation =>
            {
                if (occupation.TherapyShort == CommonConstants.PartnerString)
                    return ValidatePartnerMassages(occupation, roomNames, room);
                else
                    return true;
            }));
        }

        private static bool ValidatePartnerMassages(Occupation occupation, RoomNames roomNames, Room room)
        {
            return roomNames.PartnerRoomsRoomsAsList.Contains(room.Name);
        }
    }
}