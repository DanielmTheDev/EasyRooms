using EasyRooms.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Xps.Packaging;

namespace EasyRooms.Implementations
{
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

            const string UnicodeString = "UnicodeString";
            const string GlyphsString = "Glyphs";

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
                        if (pageContentReader.Name != GlyphsString)
                        {
                            continue;
                        }

                        if (!pageContentReader.HasAttributes)
                        {
                            continue;
                        }

                        if (pageContentReader.GetAttribute(UnicodeString) != null)
                        {
                            texts.Add(pageContentReader.GetAttribute(UnicodeString));
                        }
                    }
                    textLists.Add(texts);
                }
            }
            xpsDocument.Close();
            return textLists.Aggregate((accumulated, current) => accumulated.Concat(current).ToList());
        }
    }
}
