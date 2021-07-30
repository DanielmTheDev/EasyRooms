using EasyRooms.Extensions;
using EasyRooms.Interfaces;
using System.Windows;
using System.Linq;

namespace EasyRooms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IXpsWordsExtractor _xpsWordsExtractor;
        private readonly IRowsCreator _rowsCreator;

        public MainWindow(IXpsWordsExtractor xpsWordsExtractor, IRowsCreator rowsCreator)
        {
            InitializeComponent();
            _xpsWordsExtractor = xpsWordsExtractor;
            _rowsCreator = rowsCreator;
            TestApplication();
        }

        //todo probably extract this into its own service
        private void TestApplication()
        {
            var words = _xpsWordsExtractor
                .ExtractWords("C:\\Users\\dadam\\Google Drive\\easyRoom\\Freitag.xps")
                .RemoveHomeVisitRows()
                .RemovePageEntries()
                .RemovePauseRows()
                .RemoveHeaders()
                .RemoveLegend()
                .RemoveEnd();

            var rows = _rowsCreator.CreateRows(words);
        }
    }
}
