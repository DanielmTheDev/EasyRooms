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

        [Theory]
        [InlineData("ExpectedRows1.json", "Plan1.xps")]
        [InlineData("ExpectedRows2.json", "Plan2.xps")]
        [InlineData("ExpectedRows3.json", "Plan3.xps")]
        public void Parses_Plans(string jsonFileName, string xpsFileName)
        {
            var serializedExpectedRows = File.ReadAllText($"./IntegrationTests/TestData/{jsonFileName}");
            var expectedRows = JsonConvert.DeserializeObject<IEnumerable<Row>>(serializedExpectedRows);

            var resultRows = _dayPlanParser.ParseDayPlan($"./IntegrationTests/TestData/{xpsFileName}");

            resultRows.Should().BeEquivalentTo(expectedRows);
        }
    }
}