using WebAnchor.Tests.IntegrationTests;

namespace WebAnchor.Tests
{
    public class CustomerWithLocation : Customer
    {
        [Header("location")]
        public string Location { get; set; }
    }
}