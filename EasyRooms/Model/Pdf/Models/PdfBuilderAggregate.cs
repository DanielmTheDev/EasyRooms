using UglyToad.PdfPig.Writer;

namespace EasyRooms.Model.Pdf.Models
{
    public class PdfBuilderAggregate
    {
        public PdfPageBuilder Page { get; }
        public PdfDocumentBuilder.AddedFont Font { get; }
        public PdfDocumentBuilder.AddedFont BoldFont { get; }
        public PdfDocumentBuilder Builder { get; }
        
        public PdfBuilderAggregate(
            PdfPageBuilder page,
            PdfDocumentBuilder builder,
            PdfDocumentBuilder.AddedFont font,
            PdfDocumentBuilder.AddedFont boldFont)
        {
            Page = page;
            Font = font;
            Builder = builder;
            BoldFont = boldFont;
        }

    }
}