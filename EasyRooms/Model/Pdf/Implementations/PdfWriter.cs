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

    public void Write(IEnumerable<Room> rooms)
        => _pdfCreator
            .Create(rooms, PdfAggregateCreator.Create("Gesamtplan"))
            .ForEach(WriteFile);

    private static void WriteFile(PdfAggregate pdf)
    {
        var path = $@".\Pläne\{pdf.Name}.pdf";
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        try
        {
            var build = pdf.Builder.Build();
            File.WriteAllBytes(path, build);
        }
        catch (ArgumentException) { }
    }
}