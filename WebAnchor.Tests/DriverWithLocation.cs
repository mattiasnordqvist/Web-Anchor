using WebAnchor.Tests.IntegrationTests;

namespace WebAnchor.Tests
{
    public class DriverWithLocation : Driver
    {
        [Header("location")]
        public string Location { get; set; }
    }
}