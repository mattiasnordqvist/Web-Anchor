using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading;
using Castle.DynamicProxy;

using NUnit.Framework;

using WebAnchor.RequestFactory;

namespace WebAnchor.Tests
{
    [TestFixture]
    public class DriverTest
    {
        [Test]
        public void PlainUrl()
        {
            Test<IDriverApi>(api => api.GetDrivers(), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/driver", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlRouteSubstitution()
        {
            Test<IDriverApi>(api => api.GetDriver(8), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/driver/8", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlRouteSubstitution_CustomResolver()
        {
            Test<IDriverApi>(api => api.GetDriver2(8), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/driver/80", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams()
        {
            Test<IDriverApi>(api => api.GetDrivers(filter: "drunk"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/driver?filter=drunk", m.RequestUri.ToString());
            });
        }

        [Test]
        public void AliasAttribute()
        {
            Test<IDriverApi>(api => api.GetDrivers5(filter: "drunk"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/driver?f=drunk", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_CustomParameterNameResolver()
        {
            Test<IDriverApi>(api => api.GetDrivers2(filter: "drunk"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/driver?p_filter=drunk", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_CustomParameterValueResolver()
        {
            Test<IDriverApi>(api => api.GetDrivers3(filter: "drunk"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/driver?filter=knurd", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_MethodScopedParameterValueResolver()
        {
            Test<IDriverApi>(api => api.GetDrivers4(filter: "drunk"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/driver?filter=knurd", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_ClassScopedParameterValueResolver()
        {
            Test<IReversedApi>(api => api.GetSomething(filter: "reverseme"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/reversed?filter=emesrever", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_MethodWithList()
        {
            Test<IDriverApi>(api => api.MethodWithListParameter(new List<string> { "abc", "bcd", "cde" }), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/driver?names=abc&names=bcd&names=cde", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_MethodWithListOfInts()
        {
            Test<IDriverApi>(api => api.MethodWithIntegerListParameter(new List<int> { 1, 2, 3 }), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/driver?values=1&values=2&values=3", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_MethodWithListAndDefaultReverseResolverForStrings()
        {
            Test<IDriverApi>(api => api.MethodWithListParameter(new List<string> { "abc", "bcd", "cde" }), 
                m =>
                {
                    Assert.AreEqual(HttpMethod.Get, m.Method);
                    Assert.AreEqual("api/driver?names=cba&names=dcb&names=edc", m.RequestUri.ToString());
                },
                x => x.DefaultParameterResolvers.Add(new ReverseAttribute()));
        }

        [Test]
        public void UrlWithQueryParams_ReverseParameterOrder()
        {
            Test<IDriverApi>(api => api.MethodWithListParameter(new List<string> { "abc", "bcd", "cde" }),
                m =>
                {
                    Assert.AreEqual(HttpMethod.Get, m.Method);
                    Assert.AreEqual("api/driver?names=cde&names=bcd&names=abc", m.RequestUri.ToString());
                },
                x => x.DefaultParameterListTransformers.Add(new ReverseParameterListTransformers()));
        }

        [Test]
        public void UrlWithQueryParams_AddExtraParameter()
        {
            Test<IDriverApi>(api => api.GetDrivers("test"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/driver?filter=test&extra=3", m.RequestUri.ToString());
            },
            x => x.DefaultParameterListTransformers.Add(new AddExtraParameterTransformer("extra", 3)));
        }

        [Test]
        public void UrlWithQueryParams_QueryStringParametersAreUrlEncoded()
        {
            Test<IDriverApi>(api => api.GetDrivers("my filter?"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/driver?filter=my+filter%3F", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_RouteSegmentParametersAreUrlEncoded()
        {
            Test<IDriverApi>(api => api.GetDrivers6("my resource"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/driver/my+resource", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_CultureSensitiveParametersAreInvariantlyTransformed()
        {
            var expectedResult = WebUtility.UrlEncode("03/07/2014 00:00:00");
            RunWithCulture("en-US", () => Test<IDriverApi>(api => api.GetDrivers(new DateTime(2014, 03, 07)), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual(string.Format("api/driver?from={0}", expectedResult), m.RequestUri.ToString());
            }));
        }

        private void Test<T>(Action<T> action, Action<HttpRequestMessage> assert, Action<HttpRequestFactory> configure = null) where T : class
        {
            var api = new ProxyGenerator().CreateInterfaceProxyWithoutTarget<T>(new InvocationTester(assert, configure));
            action(api);
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
