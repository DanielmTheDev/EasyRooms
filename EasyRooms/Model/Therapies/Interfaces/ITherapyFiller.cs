namespace EasyRooms.Model.Therapies.Interfaces;

public interface ITherapyFiller
{
    void AddAllTherapies(IList<Room> rooms, List<Row> orderedRows, int bufferInMinutes, RoomNames roomNames);
}