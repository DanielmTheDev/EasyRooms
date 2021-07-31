using EasyRooms.Interfaces;
using System.Windows;

namespace EasyRooms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDayPlanParser _dayPlanParser;

        public MainWindow(IDayPlanParser dayPlanParser)
        {
            InitializeComponent();
            _dayPlanParser = dayPlanParser;
            TestApplication();
        }

        private void TestApplication()
        {
            var path = "C:\\Users\\dadam\\Google Drive\\easyRoom\\Freitag.xps";
            var rows = _dayPlanParser.ParseDayPlan(path);
        }
    }
}
