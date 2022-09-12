namespace EasyRooms.Model.Comparison.Implementations;

public interface ITherapyTypeComparer
{
    bool IsPartnerTherapy(string therapyShort);
    bool IsPreparation(string therapyShort);
    bool IsAfterTherapy(string therapyShort);
    bool IsLongTherapy(TimeSpan duration);
}