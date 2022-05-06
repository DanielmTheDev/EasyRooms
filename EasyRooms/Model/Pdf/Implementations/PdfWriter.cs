using System.Diagnostics;
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
            .Create(rooms)
            .ForEach(CreateAndOpenFile);

    private static void CreateAndOpenFile(PdfData pdf)
    {
        var path = $@".\Pläne\{pdf.Name}.pdf";
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        File.WriteAllBytes(path, pdf.Builder.Build());
        // OpenPdf(path);
    }

    private static void OpenPdf(string filePath)
    {
        var process = new Process();
        process.StartInfo.FileName = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
        process.StartInfo.Arguments = filePath;
        process.Start();
    }
}