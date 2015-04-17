using System;
using System.Net;

using Nancy.Hosting.Self;

using Newtonsoft.Json;

using NUnit.Framework;

using WebAnchor.ResponseParser;

namespace WebAnchor.Tests.IntegrationTests
{
    [TestFixture]
    public class FullTest
    {
        private const string Host = "http://localhost:1111/";
        private NancyHost _nancy;

        [SetUp]
        public void StartServer()
        {
            _nancy = new NancyHost(new HostConfiguration
            {
                                           UrlReservations = new UrlReservations { CreateAutomatically = true }
                                       },new Uri(Host));
            _nancy.Start();
        }

        [TearDown]
        public void StopServer()
        {
            _nancy.Stop();
        }

        [Test]
        public async void CreatingASimpleGetRequest()
        {
            var driverApi = Api.For<IDriverApi>(Host);
            var result = await driverApi.GetDriver(9);
            Assert.That(result.IsSuccessStatusCode);
        }

        [Test]
        public async void ParsingAJsonResponse()
        {
            var driverApi = Api.For<IDriverApi>(Host);
            var result = await driverApi.GetDriver3(9);
            Assert.AreEqual("Black Bull", result.Name);
            Assert.AreEqual(9, result.Id);
        }

        [Test]
        public async void PostingAJsonObject()
        {
            var driverApi = Api.For<IDriverApi>(Host);
            var result = await driverApi.CreateDriver2(new Driver { Id = 1, Name = "Mighty Gazelle" });
            Assert.AreEqual("Mighty Gazelle", result.Name);
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public async void PostingAJsonObject_ParsingTheLocationHeader()
        {
            var driverApi = Api.For<IDriverApi>(Host, httpResponseParser: new HttpResponseParser(new ExtendedContentDeserializer(new JsonSerializer())));
            var result = await driverApi.CreateDriverWithLocation(new Driver { Id = 1, Name = "Mighty Gazelle" });
            Assert.AreEqual("Mighty Gazelle", result.Name);
            Assert.AreEqual("api/driver/1", result.Location);
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public async void PostingAJsonObject_ParsingTheLocationHeader_SupplyingResponseParserViaSettings()
        {
            var previousResponseParser = Api.Settings.ResponseParser;
            try
            {
                Api.Settings.ResponseParser = new HttpResponseParser(new ExtendedContentDeserializer(new JsonSerializer()));
                var driverApi = Api.For<IDriverApi>(Host);
                var result = await driverApi.CreateDriverWithLocation(new Driver { Id = 1, Name = "Mighty Gazelle" });
                Assert.AreEqual("Mighty Gazelle", result.Name);
                Assert.AreEqual("api/driver/1", result.Location);
                Assert.AreEqual(1, result.Id);
            }
            finally
            {
                Api.Settings.ResponseParser = previousResponseParser;
            }
        }

        [Test]
        public async void RetrievingA404_WithTaskOFHttpResponseMessage()
        {
            var driverApi = Api.For<IDriverApi>(Host);
            var result = await driverApi.Get404();
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Test]
        [ExpectedException(typeof(ApiException))]
        public async void RetrievingA404_WithTaskOfT_ThrowsException()
        {
            var driverApi = Api.For<IDriverApi>(Host);
            await driverApi.Get404Driver();
        }

        [Test]
        [ExpectedException(typeof(JsonReaderException))]
        public async void ExpectedDataButServerReturnsNothingInContent()
        {
            var driverApi = Api.For<IDriverApi>(Host);
            await driverApi.GetAnObject();
        }
    }
}
