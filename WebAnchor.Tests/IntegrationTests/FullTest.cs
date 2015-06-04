using System;
using System.Collections.Generic;
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
                                       },
                                       new Uri(Host));
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
            var customerApi = Api.For<ICustomerApi>(Host);
            var result = await customerApi.GetCustomer(9);
            Assert.That(result.IsSuccessStatusCode);
        }

        [Test]
        public async void ParsingAJsonResponse()
        {
            var customerApi = Api.For<ICustomerApi>(Host);
            var result = await customerApi.GetCustomer3(9);
            Assert.AreEqual("Black Bull", result.Name);
            Assert.AreEqual(9, result.Id);
        }

        [Test]
        public async void PostingAJsonObject()
        {
            var customerApi = Api.For<ICustomerApi>(Host);
            var result = await customerApi.CreateCustomer2(new Customer { Id = 1, Name = "Mighty Gazelle" });
            Assert.AreEqual("Mighty Gazelle", result.Name);
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public async void PostingAnonymousObject()
        {
            var customerApi = Api.For<ICustomerApi>(Host);
            var result = await customerApi.CreateCustomer3(new { Id = 1, Name = "Anonymous Gazelle" });
            Assert.AreEqual("Anonymous Gazelle", result.Name);
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public async void PostingADeepJsonObject()
        {
            var customerApi = Api.For<ICustomerApi>(Host);
            var result = await customerApi.CreateDeepobject(new DeepObject { Deepness = 1, ShallowObject = new ShallowObject { Name = "hej" } });
            Assert.AreEqual(1, result.Deepness);
            Assert.AreEqual("hej", result.ShallowObject.Name);
        }

        [Test]
        public async void PostingADeepJsonObjectAsDictionary()
        {
            var customerApi = Api.For<ICustomerApi>(Host);
            var dictionary = new Dictionary<string, object>
            {
                { "Deepness", 1 },
                {
                    "ShallowObject",
                    new Dictionary<string, object> { { "Name", "hej" } }
                }
            };
            var result = await customerApi.CreateDeepobject2(dictionary);
            Assert.AreEqual(1, result.Deepness);
            Assert.AreEqual("hej", result.ShallowObject.Name);
        }

        [Test]
        public async void PostingAJsonObjectModifyingContentWithResolver()
        {
            var settings = new TestSettings();
            settings.ListTransformers.Add(new ContentExtender());
            var customerApi = Api.For<ICustomerApi>(Host, settings);
            var result = await customerApi.CreateCustomer2(new Customer { Id = 1, Name = "Placeholder" });
            Assert.AreEqual("Mighty Gazelle", result.Name);
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public async void TestWithTypedApi()
        {
            var customerApi = Api.For<ITypedApi<Customer>>(Host);
            var result = await customerApi.GetSameObject(1, "Mighty Gazelle");
            Assert.AreEqual("Mighty Gazelle", result.Name);
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public async void RetrievingA404_WithTaskOFHttpResponseMessage()
        {
            var customerApi = Api.For<ICustomerApi>(Host);
            var result = await customerApi.Get404();
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Test]
        [ExpectedException(typeof(ApiException))]
        public async void RetrievingA404_WithTaskOfT_ThrowsException()
        {
            var customerApi = Api.For<ICustomerApi>(Host);
            await customerApi.Get404Driver();
        }

        [Test]
        [ExpectedException(typeof(JsonReaderException))]
        public async void ExpectedDataButServerReturnsNothingInContent()
        {
            var customerApi = Api.For<ICustomerApi>(Host);
            await customerApi.GetAnObject();
        }
    }
}
