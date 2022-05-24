using System.IO;
using EasyRooms.Model.CommonExtensions;
using EasyRooms.Model.Pdf.Interfaces;
using EasyRooms.Model.Pdf.Models;

namespace EasyRooms.Model.Pdf.Implementations;

public class PdfWriter : IPdfWriter
{
    private readonly IPdfCreator _pdfCreator;

    public PdfWriter(IPdfCreator pdfCreator)
        => _pdfCreator = pdfCreator;

    public void Write(IEnumerable<Room> rooms, DateOnly date)
    {
        var pdf = _pdfCreator.Create(rooms, date);
        const string path = @".\Pläne";
        DeleteDirectoryContents(path);
        WriteFile(pdf, path);
    }

    private static void DeleteDirectoryContents(string path)
        => new DirectoryInfo(path)
            .EnumerateFiles()
            .ForEach(file => file.Delete());

    private static void WriteFile(PdfAggregate pdf, string path)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        var build = pdf.Builder.Build();
        File.WriteAllBytes($@"{path}\{pdf.Name}.pdf", build);
    }
}