using System.Collections.Generic;
using EasyRooms.Model.Models;

namespace EasyRooms.Model.Interfaces;

public interface IOccupationCreationDataProvider
{
    OccupationCreationData CalculateOccupationCreationData(string startTimeString, string duration, int bufferInMinutes, List<Room> rooms);
}