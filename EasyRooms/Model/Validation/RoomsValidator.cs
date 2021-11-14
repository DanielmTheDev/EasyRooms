using System.Collections.Generic;
using System.Linq;
using EasyRooms.Model.Constants;
using EasyRooms.Model.Rooms.Models;

namespace EasyRooms.Model.Validation
{
    public class RoomsValidator
    {
        public bool IsValid(IEnumerable<Room> rooms, RoomNames roomNames)
        {
            return rooms.All(room => room.Occupations.All(occupation =>
            {
                if (OccupationsOverlap(room))
                    return false;
                if (occupation.TherapyShort == CommonConstants.PartnerString)
                    return ValidatePartnerMassages(occupation, roomNames, room);
                else
                    return true;
            }));
        }

        //continue here, doesn't work
        private static bool OccupationsOverlap(Room room)
            => room.Occupations.All(occupation1 => !room.Occupations
                .Any(occupation2 =>
                    occupation1.StartTime < occupation2.EndTime
                    && occupation2.StartTime < occupation1.EndTime));

        private static bool ValidatePartnerMassages(Occupation occupation, RoomNames roomNames, Room room)
            => roomNames.PartnerRoomsRoomsAsList.Contains(room.Name);
    }
}