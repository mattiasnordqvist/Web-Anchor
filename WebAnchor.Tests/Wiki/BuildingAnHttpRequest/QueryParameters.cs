using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.URL;
using WebAnchor.Tests.TestUtils;
using Xunit;

namespace WebAnchor.Tests.Wiki
{
    public class QueryParameters : WebAnchorTest
    {
        [BaseLocation("posts")]
        public interface ITypicodeApi
        {
            [Get("")]
            Task<List<Post>> GetPosts(int? userId);

            [Get("?userId=2")]
            Task<List<Post>> GetPosts2();
        }

        [Fact]
        public void TestGetPosts()
        {
            TestTheRequest<ITypicodeApi>(
               api => api.GetPosts(2),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("posts?userId=2", assertMe.RequestUri.ToString());
               });

            TestTheRequest<ITypicodeApi>(
               api => api.GetPosts(null),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("posts", assertMe.RequestUri.ToString());
               });

            TestTheRequest<ITypicodeApi>(
               api => api.GetPosts2(),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("posts?userId=2", assertMe.RequestUri.ToString());
               });
        }

        public class Post
        {
        }
    }
}