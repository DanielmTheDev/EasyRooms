using System.IO;
using System.Text;
using EasyRooms.Model.CommonExtensions;
using EasyRooms.Model.Persistence.Interfaces;

namespace EasyRooms.Model.Persistence.Implementations;

internal class PlainListWriter : IPlainListWriter
{
    public void Write(IEnumerable<Room> rooms)
    {
        var lines = GetLines(rooms);
        var completeString = CreateCompleteString(lines);
        WriteFile(completeString);
    }

    private static Dictionary<string, IList<Occupation>> GetLines(IEnumerable<Room> rooms)
        => rooms.Aggregate(new Dictionary<string, IList<Occupation>>(), (dict, room) =>
        {
            dict.Add(room.Name, room.Occupations);
            return dict;
        });

    private static string CreateCompleteString(Dictionary<string, IList<Occupation>> lines)
    {
        var sb = new StringBuilder();
        lines.Keys.ForEach(key =>
        {
            sb.AppendLine($"  {key}");
            lines[key].ForEach(line => sb.AppendLine(CreateLineString(line)));
        });
        return sb.ToString();
    }

    private static string CreateLineString(Occupation line)
        => $"{(line.TouchesAdjacent ? "!" : " ")} {line.StartTime} {line.EndTime} ----- {line.Patient}";

    private static void WriteFile(string appendedString)
    {
        const string path = @".\debug_data\room_times.txt";
        Directory.CreateDirectory(Path.GetDirectoryName(path) ?? string.Empty);
        File.WriteAllText(path, appendedString);
    }
}