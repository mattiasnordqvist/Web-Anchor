using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.Content;
using WebAnchor.Attributes.URL;

namespace WebAnchor.Tests.RequestFactory.Url
{
    [BaseLocation("base")]
    public interface IApi
    {
        [Get("/path1")]
        Task<HttpResponseMessage> Get();
        
        [Get("path2")]
        Task<HttpResponseMessage> Get2();

        [Get("{path}")]
        Task<HttpResponseMessage> Get3(string path);

        [Get("a/{b}/c")]
        Task<HttpResponseMessage> Get4(string b);

        [TestVerb()]
        Task<HttpResponseMessage> TestVerb();

        [TestVerb(includeContent: true)]
        Task<HttpResponseMessage> TestVerbWithContent([Content] TestContent content);
    }

    public class TestContent
    {
        public string Message { get; set; }
    }

    public class TestVerbAttribute : HttpAttribute
    {
        private readonly bool _includeContent;

        public TestVerbAttribute(bool includeContent = false) : base(new HttpMethod("TEST"), string.Empty)
        {
            _includeContent = includeContent;
        }

        public override bool IncludeContentInRequest => _includeContent;
    }
}