using static WebAnchor.Attributes.Content.ContentAttribute;

namespace WebAnchor.Tests.RequestFactory.Transformation.Transformers.Default.AsDictionaryAttributeTest
{
    [ContentAsDictionary]
    public class AClassAsDictionary
    {
        public string A { get; set; }
    }
}