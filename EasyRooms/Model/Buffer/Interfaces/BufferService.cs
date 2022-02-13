using EasyRooms.Model.Buffer.Implementations;

namespace EasyRooms.Model.Buffer.Interfaces;

public class BufferService : IBufferService
{
    public string Buffer { get; set; }

    public BufferService()
        => Buffer = "1";
}