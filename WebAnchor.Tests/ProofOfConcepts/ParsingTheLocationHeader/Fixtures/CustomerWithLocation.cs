using WebAnchor.Tests.IntegrationTests;

namespace WebAnchor.Tests.ProofOfConcepts.ParsingTheLocationHeader.Fixtures
{
    public class CustomerWithLocation
    {
        [Header("location")]
        public string Location { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}