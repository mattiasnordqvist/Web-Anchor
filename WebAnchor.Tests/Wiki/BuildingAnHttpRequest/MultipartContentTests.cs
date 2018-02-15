using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.Content;
using WebAnchor.Attributes.URL;
using WebAnchor.TestUtils;
using Xunit;

namespace WebAnchor.Tests.Wiki
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    public class MultipartContentTests : WebAnchorTest
    {
        [BaseLocation("api")]
        public interface IApi
        {
            [Post("")]
            Task<HttpResponseMessage> PostSingleMultipart([Content][Multipart]ContentPartBase data);

            [Post("")]
            Task<HttpResponseMessage> PostParamArrayMultipart([Content][Multipart]params ContentPartBase[] data);

            [Post("")]
            Task<HttpResponseMessage> PostListMultipart([Content][Multipart]List<ContentPartBase> data);
        }

        [Fact]
        public void TestPostWithStringContent()
        {
            TestTheRequest<IApi>(
               api => api.PostSingleMultipart(new StringContentPart("Content", "Field1", "test.txt") { ContentType = "text/plain" }),
               assertMe =>
               {
                   Assert.Equal(HttpMethod.Post, assertMe.Method);
                   Assert.Equal("api", assertMe.RequestUri.ToString());
                   var body = assertMe.Content.ReadAsStringAsync().Result;
                   Assert.True(Regex.IsMatch(body, @"--(?<id>[0-9a-f\-]*)
Content-Type: text/plain
Content-Disposition: form-data; name=Field1; filename=test\.txt; filename\*=utf-8''test\.txt

Content
--\k<id>--"));
               });
        }

        [Fact]
        public void TestPostWithStreamContent()
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes("From a stream"));

            TestTheRequest<IApi>(
                api => api.PostSingleMultipart(new StreamContentPart(stream, "Field1", "test.txt") { ContentType = "text/plain" }),
                assertMe =>
                    {
                        Assert.Equal(HttpMethod.Post, assertMe.Method);
                        Assert.Equal("api", assertMe.RequestUri.ToString());
                        var body = assertMe.Content.ReadAsStringAsync().Result;
                        Assert.True(Regex.IsMatch(body, @"--(?<id>[0-9a-f\-]*)
Content-Type: text/plain
Content-Disposition: form-data; name=Field1; filename=test\.txt; filename\*=utf-8''test\.txt

From a stream
--\k<id>--"));
                    });
        }

        [Fact]
        public void TestPostWithByteArrayContent()
        {
            TestTheRequest<IApi>(
                api => api.PostSingleMultipart(new ByteArrayContentPart(Encoding.UTF8.GetBytes("From bytes"), "Field1", "test.txt") { ContentType = "text/plain" }),
                assertMe =>
                    {
                        Assert.Equal(HttpMethod.Post, assertMe.Method);
                        Assert.Equal("api", assertMe.RequestUri.ToString());
                        var body = assertMe.Content.ReadAsStringAsync().Result;
                        Assert.True(Regex.IsMatch(body, @"--(?<id>[0-9a-f\-]*)
Content-Type: text/plain
Content-Disposition: form-data; name=Field1; filename=test\.txt; filename\*=utf-8''test\.txt

From bytes
--\k<id>--"));
                    });
        }

        [Fact]
        public void TestPostWithParamArrayContentParts()
        {
            TestTheRequest<IApi>(
                api => api.PostParamArrayMultipart(
                    new StringContentPart("Content", "Field1", "test.txt") { ContentType = "text/plain" },
                    new ByteArrayContentPart(Encoding.UTF8.GetBytes("From bytes"), "Field1", "test.txt") { ContentType = "text/plain" }),
                assertMe =>
                    {
                        Assert.Equal(HttpMethod.Post, assertMe.Method);
                        Assert.Equal("api", assertMe.RequestUri.ToString());
                        var body = assertMe.Content.ReadAsStringAsync().Result;
                        Assert.True(Regex.IsMatch(body, @"--(?<id>[0-9a-f\-]*)
Content-Type: text/plain
Content-Disposition: form-data; name=Field1; filename=test\.txt; filename\*=utf-8''test\.txt

Content
--\k<id>
Content-Type: text/plain
Content-Disposition: form-data; name=Field1; filename=test\.txt; filename\*=utf-8''test\.txt

From bytes
--\k<id>--"));
                    });
        }

        [Fact]
        public void TestPostWithEnumerableTypeContentParts()
        {
            var listData = new List<ContentPartBase>
                           {
                               new StringContentPart("Content", "Field1", "test.txt") { ContentType = "text/plain" },
                               new ByteArrayContentPart(Encoding.UTF8.GetBytes("From bytes"), "Field1", "test.txt") { ContentType = "text/plain" }
                           };

            TestTheRequest<IApi>(
                api => api.PostListMultipart(listData),
                assertMe =>
                    {
                        Assert.Equal(HttpMethod.Post, assertMe.Method);
                        Assert.Equal("api", assertMe.RequestUri.ToString());
                        var body = assertMe.Content.ReadAsStringAsync().Result;
                        Assert.True(Regex.IsMatch(body, @"--(?<id>[0-9a-f\-]*)
Content-Type: text/plain
Content-Disposition: form-data; name=Field1; filename=test\.txt; filename\*=utf-8''test\.txt

Content
--\k<id>
Content-Type: text/plain
Content-Disposition: form-data; name=Field1; filename=test\.txt; filename\*=utf-8''test\.txt

From bytes
--\k<id>--"));
                    });
        }

        public class Post
        {
            public int Id { get; set; }
            public int UserId { get; set; }
        }
    }
}