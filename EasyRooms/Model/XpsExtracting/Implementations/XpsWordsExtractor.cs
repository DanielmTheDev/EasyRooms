using System.IO;
using System.Windows.Xps.Packaging;
using EasyRooms.Model.XpsExtracting.Interfaces;

#nullable disable
namespace EasyRooms.Model.XpsExtracting.Implementations;

public class XpsWordsExtractor : IXpsWordsExtractor
{
    public IEnumerable<string> ExtractWords(string filePath)
    {
        var xpsDocument = new XpsDocument(filePath, FileAccess.Read);
        var fixedDocSeqReader = xpsDocument.FixedDocumentSequenceReader;
        if (fixedDocSeqReader == null)
        {
            return null;
        }

        const string unicodeString = "UnicodeString";
        const string glyphsString = "Glyphs";

        var textLists = new List<List<string>>();
        foreach (var fixedDocumentReader in fixedDocSeqReader.FixedDocuments)
        {
            foreach (var pageReader in fixedDocumentReader.FixedPages)
            {
                var pageContentReader = pageReader.XmlReader;
                if (pageContentReader == null)
                {
                    continue;
                }

                var texts = new List<string>();
                while (pageContentReader.Read())
                {
                    if (pageContentReader.Name != glyphsString)
                    {
                        continue;
                    }

                    if (!pageContentReader.HasAttributes)
                    {
                        continue;
                    }

                    if (pageContentReader.GetAttribute(unicodeString) != null)
                    {
                        texts.Add(pageContentReader.GetAttribute(unicodeString));
                    }
                }
                textLists.Add(texts);
            }
        }
        xpsDocument.Close();
        return textLists.Aggregate((accumulated, current) => accumulated.Concat(current).ToList());
    }
}