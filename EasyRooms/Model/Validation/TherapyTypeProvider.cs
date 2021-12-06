using System;
using EasyRooms.Model.Constants;
using EasyRooms.Model.Rooms.Models;

namespace EasyRooms.Model.Validation
{
    public static class TherapyTypeProvider
    {
        public static bool IsPartnerTherapy(Occupation occupation) 
            => string.Equals(occupation.TherapyShort, CommonConstants.PartnerString, StringComparison.InvariantCultureIgnoreCase);
    }
}