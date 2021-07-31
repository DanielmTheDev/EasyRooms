using EasyRooms.Implementations;
using EasyRooms.Interfaces;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace EasyRooms.Tests
{
    public class DayPlanParserTests
    {
        private DayPlanParser _dayPlanParser;

        public DayPlanParserTests()
        {
            _dayPlanParser = new DayPlanParser(new XpsWordsExtractor(), new RowsCreator());
        }

        [Fact]
        public void Parses_Plans()
        {
            var serializedExpectedRows = File.ReadAllText("./IntegrationTests/TestData/ExpectedRows1.json");
            var expectedRows = JsonConvert.DeserializeObject<IEnumerable<Row>>(serializedExpectedRows);

            var resultRows = _dayPlanParser.ParseDayPlan("./IntegrationTests/TestData/Plan1.xps");

            resultRows.Should().BeEquivalentTo(expectedRows);
        }
    }
}
