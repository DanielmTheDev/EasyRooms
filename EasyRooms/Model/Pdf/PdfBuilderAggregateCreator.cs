using System.IO;
using EasyRooms.Model.Pdf.Models;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Writer;

namespace EasyRooms.Model.Pdf;

public static class PdfBuilderAggregateCreator
{
    public static PdfBuilderAggregate Create()
    {
        var builder = new PdfDocumentBuilder();
        var fontFile = File.ReadAllBytes(@".\Assets\OpenSans-Regular.ttf");
        var boldFontFile = File.ReadAllBytes(@".\Assets\OpenSans-Bold.ttf");
        var font = builder.AddTrueTypeFont(fontFile);
        var boldFont = builder.AddTrueTypeFont(boldFontFile);
        var page = builder.AddPage(PageSize.A4);
        return new PdfBuilderAggregate(page, builder, font, boldFont);
    }
}