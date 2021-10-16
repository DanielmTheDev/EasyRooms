using System.Collections.Generic;
using EasyRooms.Model.Models;

namespace EasyRooms.Model.Interfaces;

public interface ITherapyFiller
{
    void AddPartnerTherapies(List<Room> rooms, List<Row> orderedRows, int bufferInMinutes);
    void AddNormalTherapies(List<Room> rooms, List<Row> orderedRows, int bufferInMinutes);
}