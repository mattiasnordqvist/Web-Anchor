using System.Collections.Generic;
using System.Linq;

using Xunit;

using WebAnchor.RequestFactory.Transformation.Transformers.Default;
using WebAnchor.Tests.ProofOfConcepts.GeckoBoardBarChart.BarChart;

namespace WebAnchor.Tests.ProofOfConcepts.GeckoBoardBarChart
{
    public class Tests
    {
        [Fact]
        public void Test1()
        {
            var dict = BarChartTest().ToDictionary();
            var shouldBeAListOfObjects = (IEnumerable<object>)dict["series"];
            var shouldBeADictionary = (IDictionary<string, object>)shouldBeAListOfObjects.First();
            Assert.True(shouldBeADictionary.ContainsKey("data"));
            Assert.True(shouldBeADictionary["data"] is IEnumerable<int>);
        }

        private static MinimumOfGeckoBarChartToReproduceError BarChartTest()
        {
            var series = new List<BarDataItem>
            {
                new BarDataItem
                {
                    data = new List<int> { 50, 60, 70 }
                }
            };

            return new MinimumOfGeckoBarChartToReproduceError
            {
                series = series
            };
        }
    }
}
