using EasyRooms.Extensions;
using EasyRooms.Interfaces;
using System.Windows;

namespace EasyRooms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IXpsWordsExtractor _xpsWordsExtractor;
        private readonly IWordListTrimmer _wordListTrimmer;
        private readonly IPauseRowsRemover _pauseRowsRemover;
        private readonly IHomeVisitRowsRemover _homeVisitRowsRemover;
        private readonly IRowsCreator _rowsCreator;

        public MainWindow(
            IXpsWordsExtractor xpsWordsExtractor,
            IPauseRowsRemover pauseRowsRemover,
            IHomeVisitRowsRemover homeVisitRowsRemover,
            IRowsCreator rowsCreator,
            IWordListTrimmer wordListTrimmer)
        {
            InitializeComponent();
            _xpsWordsExtractor = xpsWordsExtractor;
            _pauseRowsRemover = pauseRowsRemover;
            _homeVisitRowsRemover = homeVisitRowsRemover;
            _rowsCreator = rowsCreator;
            _wordListTrimmer = wordListTrimmer;
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
