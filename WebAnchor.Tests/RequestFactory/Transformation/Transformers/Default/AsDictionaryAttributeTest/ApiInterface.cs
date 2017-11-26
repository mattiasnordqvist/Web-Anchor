using WebAnchor.Attributes.Content;
using WebAnchor.Attributes.URL;
using static WebAnchor.Attributes.Content.ContentAttribute;

namespace WebAnchor.Tests.RequestFactory.Transformation.Transformers.Default.AsDictionaryAttributeTest
{
    [ContentAsDictionary]
    public interface IApiInterfaceWithAttribute
    {
        [Post("")]
        void NoAttribute([Content] object unknown);
    }

    public interface IApiInterface
    {
        [Post("")]
        void NoAttribute([Content] object unknown);

        [Post("")]
        void AttributeOnParameter([Content][ContentAsDictionary] object unknown);

        [Post("")]
        [ContentAsDictionary]
        void AttributeOnMethod([Content] object unknown);
    }
}
