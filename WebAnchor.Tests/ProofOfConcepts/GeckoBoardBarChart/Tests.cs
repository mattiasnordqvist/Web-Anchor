using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using PushApiTest.Entities.Gecko.BarChart;

using WebAnchor.RequestFactory.Transformation.Transformers.Default;

namespace WebAnchor.Tests.ProofOfConcepts.GeckoBoardBarChart
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var dict = BarChartTest().ToDictionary();
            var shouldBeAListOfObjects = (IEnumerable<object>)dict["series"];
            var shouldBeADictionary = (IDictionary<string, object>)shouldBeAListOfObjects.First();
            Assert.IsTrue(shouldBeADictionary.ContainsKey("data"));
            Assert.IsTrue(shouldBeADictionary["data"] is IEnumerable<int>);
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
