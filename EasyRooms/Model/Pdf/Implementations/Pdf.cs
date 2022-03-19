using System.IO;
using EasyRooms.Model.Pdf.Models;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Writer;

namespace EasyRooms.Model.Pdf.Implementations;

public static class Pdf
{
    public static PdfData Create(string fileName)
    {
        var builder = new PdfDocumentBuilder();
        var fontFile = File.ReadAllBytes(@".\Assets\OpenSans-Regular.ttf");
        var boldFontFile = File.ReadAllBytes(@".\Assets\OpenSans-Bold.ttf");
        var font = builder.AddTrueTypeFont(fontFile);
        var boldFont = builder.AddTrueTypeFont(boldFontFile);
        var page = builder.AddPage(PageSize.A4);
        return new PdfData(fileName, page, builder, font, boldFont);
    }
}