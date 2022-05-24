using EasyRooms.Model.Constants;

namespace EasyRooms.Model.Validation.Implementations;

public static class TherapyTypeComparer
{
    public static bool IsPartnerTherapy(string therapyShort)
        => string.Equals(therapyShort, CommonConstants.PartnerString, StringComparison.InvariantCultureIgnoreCase);

    public static bool IsPreparation(string therapyShort)
        => string.Equals(therapyShort, CommonConstants.PreparationString, StringComparison.InvariantCultureIgnoreCase);

    public static bool IsAfterTherapy(string therapyShort)
        => string.Equals(therapyShort, CommonConstants.AfterString, StringComparison.InvariantCultureIgnoreCase);

    public static bool IsLongTherapy(TimeSpan duration)
        => duration >= TimeSpan.FromMinutes(60);

    public static bool IsTimeSpecificTherapy(TimeOnly startTime, TimeOnly endTime, TimeOnly startTimeLimit, TimeOnly endTimeLimit)
        => startTime >= startTimeLimit && endTime <= endTimeLimit;
}