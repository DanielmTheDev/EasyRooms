using EasyRooms.Model.Constants;
using EasyRooms.Model.Persistence.Interfaces;

namespace EasyRooms.Model.Comparison.Implementations;

public class TherapyTypeComparer : ITherapyTypeComparer
{
    private readonly IOptionsPersister _optionsPersister;

    public TherapyTypeComparer(IOptionsPersister optionsPersister)
        => _optionsPersister = optionsPersister;

    public bool IsPartnerTherapy(string therapyShort)
        => string.Equals(therapyShort, _optionsPersister.SavedOptions.Rooms.PartnerTherapyName, StringComparison.InvariantCultureIgnoreCase);

    public bool IsPreparation(string therapyShort)
        => string.Equals(therapyShort, CommonConstants.PreparationString, StringComparison.InvariantCultureIgnoreCase);

    public bool IsAfterTherapy(string therapyShort)
        => string.Equals(therapyShort, CommonConstants.AfterString, StringComparison.InvariantCultureIgnoreCase);

    public bool IsLongTherapy(TimeSpan duration)
        => duration >= TimeSpan.FromMinutes(60);
}