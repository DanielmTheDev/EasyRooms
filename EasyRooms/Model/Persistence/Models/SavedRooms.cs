namespace EasyRooms.Model.Persistence.Models;

public class SavedRooms
{
    //todo rename these properties, maybe even make arrays out of them and convert them when deserializing
    public string PartnerRooms { get; set; } = string.Empty;
    public string PartnerTherapyName { get; set; } = string.Empty;
    public string Rooms { get; set; } = string.Empty;
    public string MassagesForSpecificRooms { get; set; } = string.Empty;
}