using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;

using WebAnchor.RequestFactory;
using WebAnchor.Tests.ACollectionOfRandomTests.Fixtures;
using WebAnchor.Tests.RequestFactory.Transformation.Custom;
using WebAnchor.Tests.RequestFactory.Transformation.Transformers.Attribute.Fixtures;
using WebAnchor.Tests.TestUtils;

using Xunit;

namespace WebAnchor.Tests.ACollectionOfRandomTests
{
    public class Tests : WebAnchorTest
    {
        [Fact]
        public void PlainUrl()
        {
            TestTheRequest<ITestApi>(api => api.GetCustomers(), m =>
            {
                Assert.Equal(HttpMethod.Get, m.Method);
                Assert.Equal("api/customer", m.RequestUri.ToString());
            });
        }

        [Fact]
        public void PostWithoutPayload()
        {
            TestTheRequest<ITestApi>(api => api.PostWithoutPayload(), m =>
            {
                Assert.Equal(HttpMethod.Post, m.Method);
                Assert.Equal("api/customer", m.RequestUri.ToString());
            });
        }

        [Fact]
        public void UrlRouteSubstitution()
        {
            TestTheRequest<ITestApi>(api => api.GetCustomer(8), m =>
            {
                Assert.Equal(HttpMethod.Get, m.Method);
                Assert.Equal("api/customer/8", m.RequestUri.ToString());
            });
        }

        [Fact]
        public void BaseLocationSubstitution()
        {
            TestTheRequest<IBaseLocationSubstitution>(api => api.Get(),
                configure: x => x.ParameterListTransformers.Add(new AddExtraParameterTransformer("version", "v2", ParameterType.Route)),
                assertHttpRequestMessage: m =>
                {
                    Assert.Equal(HttpMethod.Get, m.Method);
                    Assert.Equal("api/v2", m.RequestUri.ToString());
                });
        }

        [Fact]
        public void PlainUrlWithFixedQueryStringParam()
        {
            TestTheRequest<ITestApi>(api => api.GetCustomers7("hej"), m =>
            {
                Assert.Equal(HttpMethod.Get, m.Method);
                Assert.Equal("api/customer?extraParam=7&filter=hej", m.RequestUri.ToString());
            });
        }

        [Fact]
        public void UrlWithQueryParams()
        {
            TestTheRequest<ITestApi>(api => api.GetCustomers(filter: "drunk"), m =>
            {
                Assert.Equal(HttpMethod.Get, m.Method);
                Assert.Equal("api/customer?filter=drunk", m.RequestUri.ToString());
            });
        }

        [Fact]
        public void AliasAttribute()
        {
            TestTheRequest<ITestApi>(api => api.GetCustomers5(filter: "drunk"), m =>
            {
                Assert.Equal(HttpMethod.Get, m.Method);
                Assert.Equal("api/customer?f=drunk", m.RequestUri.ToString());
            });
        }

        [Fact]
        public void UrlWithQueryParams_MethodScopedParameterValueResolver()
        {
            TestTheRequest<ITestApi>(api => api.GetCustomers4(filter: "drunk"), m =>
            {
                Assert.Equal(HttpMethod.Get, m.Method);
                Assert.Equal("api/customer?filter=knurd", m.RequestUri.ToString());
            });
        }

        [Fact]
        public void UrlWithQueryParams_ClassScopedParameterValueResolver()
        {
            TestTheRequest<IReversedApi>(api => api.GetSomething(filter: "reverseme"), m =>
            {
                Assert.Equal(HttpMethod.Get, m.Method);
                Assert.Equal("api/reversed?filter=emesrever", m.RequestUri.ToString());
            });
        }

        [Fact]
        public void UrlWithQueryParams_MethodWithList()
        {
            TestTheRequest<ITestApi>(api => api.MethodWithListParameter(new List<string> { "abc", "bcd", "cde" }), m =>
            {
                Assert.Equal(HttpMethod.Get, m.Method);
                Assert.Equal("api/customer?names=abc&names=bcd&names=cde", m.RequestUri.ToString());
            });
        }

        [Fact]
        public void UrlWithQueryParams_MethodWithListOfInts()
        {
            TestTheRequest<ITestApi>(api => api.MethodWithIntegerListParameter(new List<int> { 1, 2, 3 }), m =>
            {
                Assert.Equal(HttpMethod.Get, m.Method);
                Assert.Equal("api/customer?values=1&values=2&values=3", m.RequestUri.ToString());
            });
        }

        [Fact]
        public void UrlWithQueryParams_MethodWithArrayOfInts()
        {
            TestTheRequest<ITestApi>(api => api.MethodWithIntegerArrayParameter(new[] { 1, 2, 3 }), m =>
            {
                Assert.Equal(HttpMethod.Get, m.Method);
                Assert.Equal("api/customer?values=1&values=2&values=3", m.RequestUri.ToString());
            });
        }

        [Fact]
        public void UrlWithQueryParams_MethodWithListAndDefaultReverseResolverForStrings()
        {
            TestTheRequest<ITestApi>(
                api => api.MethodWithListParameter(new List<string> { "abc", "bcd", "cde" }),
                m =>
                    {
                        Assert.Equal(HttpMethod.Get, m.Method);
                        Assert.Equal("api/customer?names=cba&names=dcb&names=edc", m.RequestUri.ToString());
                    },
            x => x.ParameterListTransformers.Add(new ReverseAttribute()));
        }

        [Fact]
        public void UrlWithQueryParams_QueryStringParametersAreUrlEncoded()
        {
            TestTheRequest<ITestApi>(api => api.GetCustomers("my filter?"), m =>
            {
                Assert.Equal(HttpMethod.Get, m.Method);
                Assert.Equal("api/customer?filter=my+filter%3F", m.RequestUri.ToString());
            });
        }

        [Fact]
        public void UrlWithQueryParams_RouteSegmentParametersAreUrlEncoded()
        {
            TestTheRequest<ITestApi>(api => api.GetCustomers6("my resource"), m =>
            {
                Assert.Equal(HttpMethod.Get, m.Method);
                Assert.Equal("api/customer/my+resource", m.RequestUri.ToString());
            });
        }

        [Fact]
        public void UrlWithQueryParams_CultureSensitiveParametersAreInvariantlyTransformed()
        {
            var expectedResult = WebUtility.UrlEncode("03/07/2014 00:00:00");
            RunWithCulture("en-US", () => TestTheRequest<ITestApi>(api => api.GetCustomers(new DateTime(2014, 03, 07)), m =>
            {
                Assert.Equal(HttpMethod.Get, m.Method);
                Assert.Equal($"api/customer?from={expectedResult}", m.RequestUri.ToString());
            }));
        }

        private void RunWithCulture(string cultureName, Action action)
        {
            var previousCulture = CultureInfo.CurrentCulture;
            CultureInfo.CurrentCulture = new CultureInfo(cultureName);
            try
            {
                action();
            }
            finally
            {
                CultureInfo.CurrentCulture = previousCulture;
            }
        }
    }
}
