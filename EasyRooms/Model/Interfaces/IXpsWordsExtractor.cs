using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRooms.Interfaces
{
    public interface IXpsWordsExtractor
    {
        IEnumerable<string> ExtractWords(string filePath);
    }
}
