using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using WebAnchor.Attributes.Content;
using WebAnchor.Attributes.URL;
using WebAnchor.TestUtils;
using Xunit;

namespace WebAnchor.Tests.Wiki
{
    public class MultipartContentTests : WebAnchorTest
    {
        [BaseLocation("api")]
        public interface IApi
        {
            [Post("")]
            Task<HttpResponseMessage> PostSingleMultipart([Content][Multipart] ContentPartBase data);

            [Post("")]
            Task<HttpResponseMessage> PostParamArrayMultipart([Content][Multipart] params ContentPartBase[] data);

            [Post("")]
            Task<HttpResponseMessage> PostListMultipart([Content][Multipart] List<ContentPartBase> data);
        }

        [Fact]
        public void TestPostWithStringContent()
        {
            TestTheRequest<IApi>(
               api => api.PostSingleMultipart(new StringContentPart("Content", "Field1", "test.txt") { ContentType = "text/plain" }),
               assertMe =>
                   {
                       VerifyMultipartRequest(
                           assertMe,
                           @"--{boundary}
Content-Type: text/plain
Content-Disposition: form-data; name=Field1; filename=test.txt; filename*=utf-8''test.txt

Content
--{boundary}--
");
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
                        VerifyMultipartRequest(
                            assertMe,
                            @"--{boundary}
Content-Type: text/plain
Content-Disposition: form-data; name=Field1; filename=test.txt; filename*=utf-8''test.txt

From a stream
--{boundary}--
");
                    });
        }

        [Fact]
        public void TestPostWithByteArrayContent()
        {
            TestTheRequest<IApi>(
                api => api.PostSingleMultipart(new ByteArrayContentPart(Encoding.UTF8.GetBytes("From bytes"), "Field1", "test.txt") { ContentType = "text/plain" }),
                assertMe =>
                    {
                        VerifyMultipartRequest(
                             assertMe,
                             @"--{boundary}
Content-Type: text/plain
Content-Disposition: form-data; name=Field1; filename=test.txt; filename*=utf-8''test.txt

From bytes
--{boundary}--
");
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
                        VerifyMultipartRequest(
                            assertMe,
                            @"--{boundary}
Content-Type: text/plain
Content-Disposition: form-data; name=Field1; filename=test.txt; filename*=utf-8''test.txt

Content
--{boundary}
Content-Type: text/plain
Content-Disposition: form-data; name=Field1; filename=test.txt; filename*=utf-8''test.txt

From bytes
--{boundary}--
");
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
                        VerifyMultipartRequest(
                            assertMe,
                            @"--{boundary}
Content-Type: text/plain
Content-Disposition: form-data; name=Field1; filename=test.txt; filename*=utf-8''test.txt

Content
--{boundary}
Content-Type: text/plain
Content-Disposition: form-data; name=Field1; filename=test.txt; filename*=utf-8''test.txt

From bytes
--{boundary}--
");
                    });
        }

        private static void VerifyMultipartRequest(HttpRequestMessage assertMe, string expectedContent)
        {
            Assert.Equal(HttpMethod.Post, assertMe.Method);
            Assert.Equal("api", assertMe.RequestUri?.ToString());

            var boundary = assertMe.Content?.Headers.ContentType?.Parameters.SingleOrDefault(p => p.Name == "boundary")?.Value;
            Assert.NotNull(boundary);
            Assert.Matches(@"^""[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}""$", boundary);

            var body = assertMe.Content?.ReadAsStringAsync().Result;
            Assert.Equal(
                expectedContent.Replace("{boundary}", boundary!.Trim('"')),
                body,
                ignoreLineEndingDifferences: true);
        }

        public class Post
        {
            public int Id { get; set; }
            public int UserId { get; set; }
        }
    }
}