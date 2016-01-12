using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using PushApiTest.Entities.Gecko.BarChart;

namespace WebAnchor.Tests.ProofOfConcepts.GeckoBoardBarChart.BarChart
{
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Because we want to test that web anchor works with inconsistent naming.")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Because we want to test that web anchor works with inconsistent naming.")]
    public class MinimumOfGeckoBarChartToReproduceError
    {
        public XAxis x_axis { get; set; }
        public List<BarDataItem> series { get; set; } 
    }
}
