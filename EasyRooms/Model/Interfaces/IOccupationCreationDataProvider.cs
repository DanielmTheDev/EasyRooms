using System.Collections.Generic;
using EasyRooms.Model.Models;

namespace EasyRooms.Model.Interfaces;

public interface IOccupationCreationDataProvider
{
    OccupationCreationData GetOccupationCreationData(string startTimeString, string duration, int bufferInMinutes, List<Room> rooms);
}