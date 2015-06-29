using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading;

using NUnit.Framework;

using WebAnchor.Tests.ACollectionOfRandomTests.Fixtures;
using WebAnchor.Tests.RequestFactory.Transformation.Transformers.Attribute.Fixtures;
using WebAnchor.Tests.TestUtils;

namespace WebAnchor.Tests.ACollectionOfRandomTests
{
    [TestFixture]
    public class Tests : WebAnchorTest
    {
        [Test]
        public void PlainUrl()
        {
            TestTheRequestMessage<ITestApi>(api => api.GetCustomers(), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer", m.RequestUri.ToString());
            });
        }

        [Test]
        public void PostWithoutPayload()
        {
            TestTheRequestMessage<ITestApi>(api => api.PostWithoutPayload(), m =>
            {
                Assert.AreEqual(HttpMethod.Post, m.Method);
                Assert.AreEqual("api/customer", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlRouteSubstitution()
        {
            TestTheRequestMessage<ITestApi>(api => api.GetCustomer(8), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer/8", m.RequestUri.ToString());
            });
        }

        [Test]
        public void PlainUrlWithFixedQueryStringParam()
        {
            TestTheRequestMessage<ITestApi>(api => api.GetCustomers7("hej"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?extraParam=7&filter=hej", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams()
        {
            TestTheRequestMessage<ITestApi>(api => api.GetCustomers(filter: "drunk"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?filter=drunk", m.RequestUri.ToString());
            });
        }

        [Test]
        public void AliasAttribute()
        {
            TestTheRequestMessage<ITestApi>(api => api.GetCustomers5(filter: "drunk"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?f=drunk", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_MethodScopedParameterValueResolver()
        {
            TestTheRequestMessage<ITestApi>(api => api.GetCustomers4(filter: "drunk"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?filter=knurd", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_ClassScopedParameterValueResolver()
        {
            TestTheRequestMessage<IReversedApi>(api => api.GetSomething(filter: "reverseme"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/reversed?filter=emesrever", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_MethodWithList()
        {
            TestTheRequestMessage<ITestApi>(api => api.MethodWithListParameter(new List<string> { "abc", "bcd", "cde" }), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?names=abc&names=bcd&names=cde", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_MethodWithListOfInts()
        {
            TestTheRequestMessage<ITestApi>(api => api.MethodWithIntegerListParameter(new List<int> { 1, 2, 3 }), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?values=1&values=2&values=3", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_MethodWithArrayOfInts()
        {
            TestTheRequestMessage<ITestApi>(api => api.MethodWithIntegerArrayParameter(new[] { 1, 2, 3 }), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?values=1&values=2&values=3", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_MethodWithListAndDefaultReverseResolverForStrings()
        {
            TestTheRequestMessage<ITestApi>(
                api => api.MethodWithListParameter(new List<string> { "abc", "bcd", "cde" }),
                m =>
                    {
                        Assert.AreEqual(HttpMethod.Get, m.Method);
                        Assert.AreEqual("api/customer?names=cba&names=dcb&names=edc", m.RequestUri.ToString());
                    },
            x => x.ParameterListTransformers.Add(new ReverseAttribute()));
        }

        [Test]
        public void UrlWithQueryParams_QueryStringParametersAreUrlEncoded()
        {
            TestTheRequestMessage<ITestApi>(api => api.GetCustomers("my filter?"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?filter=my+filter%3F", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_RouteSegmentParametersAreUrlEncoded()
        {
            TestTheRequestMessage<ITestApi>(api => api.GetCustomers6("my resource"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer/my+resource", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_CultureSensitiveParametersAreInvariantlyTransformed()
        {
            var expectedResult = WebUtility.UrlEncode("03/07/2014 00:00:00");
            RunWithCulture("en-US", () => TestTheRequestMessage<ITestApi>(api => api.GetCustomers(new DateTime(2014, 03, 07)), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual(string.Format("api/customer?from={0}", expectedResult), m.RequestUri.ToString());
            }));
        }

        private void RunWithCulture(string cultureName, Action action)
        {
            var previousCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            try
            {
                action();
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = previousCulture;
            }
        }
    }
}
