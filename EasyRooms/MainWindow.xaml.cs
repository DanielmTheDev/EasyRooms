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

        //TODO extract stuff into extension methods
        private void TestApplication()
        {
            var words = _xpsWordsExtractor.ExtractWords("C:\\Users\\dadam\\Google Drive\\easyRoom\\gesamtPlan.xps");
            var trimmedWords = _wordListTrimmer.TrimList(words);
            var wordsWithoutPause = _pauseRowsRemover.RemovePauseRows(trimmedWords);
            var wordsWithoutHomeVisit = _homeVisitRowsRemover.RemoveHomeVisitRows(wordsWithoutPause);
            var rows = _rowsCreator.CreateRows(wordsWithoutHomeVisit);
        }
    }
}
