using EasyRooms.Interfaces;
using System;
using System.Windows;

namespace EasyRooms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IXpsWordsExtractor _xpsWordsExtractor;
        private readonly IPauseRowsRemover _pauseRowsRemover;
        private readonly IHomeVisitRowsRemover _homeVisitRowsRemover;
        private readonly IRowsCreator _rowsCreator;
        private readonly IWordListTrimmer _wordListTrimmer;

        public MainWindow(
            IXpsWordsExtractor xpsWordsExtractor, 
            IPauseRowsRemover pauseRowsRemover,
            IHomeVisitRowsRemover homeVisitRowsRemover,
            IRowsCreator rowsCreator,
            IWordListTrimmer wordListTrimmer
            )
        {
            InitializeComponent();
            _xpsWordsExtractor = xpsWordsExtractor;
            _pauseRowsRemover = pauseRowsRemover;
            _homeVisitRowsRemover = homeVisitRowsRemover;
            _rowsCreator = rowsCreator;
            _wordListTrimmer = wordListTrimmer;

            Console.WriteLine("test");
            Console.WriteLine("test");
        }
    }
}
