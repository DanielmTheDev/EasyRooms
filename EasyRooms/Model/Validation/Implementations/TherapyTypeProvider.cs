using System;
using EasyRooms.Model.Constants;

namespace EasyRooms.Model.Validation.Implementations;

public static class TherapyTypeProvider
{
    public static bool IsPartnerTherapy(string therapyShort)
        => string.Equals(therapyShort, CommonConstants.PartnerString, StringComparison.InvariantCultureIgnoreCase);

    public static bool IsPreparation(string therapyShort)
        => string.Equals(therapyShort, CommonConstants.PreparationString, StringComparison.InvariantCultureIgnoreCase);

    public static bool IsAfterTherapy(string therapyShort)
        => string.Equals(therapyShort, CommonConstants.AfterString, StringComparison.InvariantCultureIgnoreCase);
}