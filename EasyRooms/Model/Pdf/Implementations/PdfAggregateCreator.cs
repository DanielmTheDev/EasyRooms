using System.IO;
using EasyRooms.Model.Pdf.Models;
using UglyToad.PdfPig.Writer;

namespace EasyRooms.Model.Pdf.Implementations;

public static class PdfAggregateCreator
{
    public static PdfAggregate Create(string fileName)
    {
        var builder = new PdfDocumentBuilder();
        var fontFile = File.ReadAllBytes(@".\Assets\OpenSans-Regular.ttf");
        var boldFontFile = File.ReadAllBytes(@".\Assets\OpenSans-Bold.ttf");
        var font = builder.AddTrueTypeFont(fontFile);
        var boldFont = builder.AddTrueTypeFont(boldFontFile);
        return new(fileName, builder, font, boldFont);
    }
}