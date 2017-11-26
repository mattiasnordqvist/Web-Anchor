using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.URL;
using WebAnchor.Tests.TestUtils;
using Xunit;

namespace WebAnchor.Tests.Wiki
{
    public class BuildingAnUrl : WebAnchorTest
    {

        [Fact]
        public void TestGetPosts()
        {
            TestTheRequest<ITypicodeApi>(
               api => api.GetPosts(),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("posts", assertMe.RequestUri.ToString());
               });
        }

        [Fact]
        public void TestGetPost()
        {
            TestTheRequest<ITypicodeApi>(
               api => api.GetPost(291),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Get, assertMe.Method);
                   Assert.Equal("posts/291", assertMe.RequestUri.ToString());
               });
        }

        [Fact]
        public void TestDeletePost()
        {
            TestTheRequest<ITypicodeApi>(
               api => api.DeletePost(291),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Delete, assertMe.Method);
                   Assert.Equal("posts/291", assertMe.RequestUri.ToString());
               });
        }

        [BaseLocation("posts")]
        public interface ITypicodeApi
        {
            [Get("")]
            Task<List<Post>> GetPosts();

            [Get("{id}")]
            Task<Post> GetPost(int id);

            [Delete("{id}")]
            Task<HttpResponseMessage> DeletePost(int id);
        }

        public class Post
        {
        }
    }

  
}
