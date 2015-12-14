using System.Collections.Generic;

namespace PushApiTest.Entities.Gecko.BarChart
{
    public class MinimumOfGeckoBarChartToReproduceError
    {
        public XAxis x_axis { get; set; }
        public List<BarDataItem> series { get; set; } 
    }
}
