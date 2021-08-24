using EasyRooms.Interfaces;
using Newtonsoft.Json;
using System.IO;
using System.Windows;

namespace EasyRooms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDayPlanParser _dayPlanParser;
        private readonly IRoomOccupationsFiller _occupationsFiller;

        public MainWindow(IDayPlanParser dayPlanParser, IRoomOccupationsFiller occupationsFiller)
        {
            InitializeComponent();
            _dayPlanParser = dayPlanParser;
            _occupationsFiller = occupationsFiller;
            TestApplication();
        }

        private void TestApplication()
        {
            var path = @"C:\Repos\EasyRooms\EasyRooms.Tests\IntegrationTests\TestData\Plan2.xps";
            var rows = _dayPlanParser.ParseDayPlan(path);
            var roomsStrings = new[] { "room1", "room2", "room3", "room4", "room5" };
            var rooms = _occupationsFiller.FillRoomOccupations(rows, roomsStrings);
            var serializedRooms = JsonConvert.SerializeObject(rooms, Formatting.Indented);
            File.WriteAllText(@"C:\Repos\EasyRooms\EasyRooms.Tests\IntegrationTests\TestData\rooms.json", serializedRooms);
        }
    }
}
