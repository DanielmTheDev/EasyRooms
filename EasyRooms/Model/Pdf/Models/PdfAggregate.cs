using UglyToad.PdfPig.Writer;

namespace EasyRooms.Model.Pdf.Models;

public class PdfAggregate
{
    public string Name { get;  }
    public PdfDocumentBuilder.AddedFont Font { get; }
    public PdfDocumentBuilder.AddedFont BoldFont { get; }
    public PdfDocumentBuilder Builder { get; }

    public PdfAggregate(
        string name,
        PdfDocumentBuilder builder,
        PdfDocumentBuilder.AddedFont font,
        PdfDocumentBuilder.AddedFont boldFont)
    {
        Font = font;
        Builder = builder;
        BoldFont = boldFont;
        Name = name;
    }
}