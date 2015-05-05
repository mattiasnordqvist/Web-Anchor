using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading;

using NUnit.Framework;

namespace WebAnchor.Tests
{
    [TestFixture]
    public class CustomerTest : WebAnchorTest
    {
        [Test]
        public void PlainUrl()
        {
            Test<ICustomerApi>(api => api.GetCustomers(), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlRouteSubstitution()
        {
            Test<ICustomerApi>(api => api.GetCustomer(8), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer/8", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlRouteSubstitution_CustomResolver()
        {
            Test<ICustomerApi>(api => api.GetCustomer2(8), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer/80", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams()
        {
            Test<ICustomerApi>(api => api.GetCustomers(filter: "drunk"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?filter=drunk", m.RequestUri.ToString());
            });
        }

        [Test]
        public void AliasAttribute()
        {
            Test<ICustomerApi>(api => api.GetCustomers5(filter: "drunk"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?f=drunk", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_CustomParameterNameResolver()
        {
            Test<ICustomerApi>(api => api.GetCustomers2(filter: "drunk"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?p_filter=drunk", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_CustomParameterValueResolver()
        {
            Test<ICustomerApi>(api => api.GetCustomers3(filter: "drunk"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?filter=knurd", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_MethodScopedParameterValueResolver()
        {
            Test<ICustomerApi>(api => api.GetCustomers4(filter: "drunk"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?filter=knurd", m.RequestUri.ToString());
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
            Test<ICustomerApi>(api => api.MethodWithListParameter(new List<string> { "abc", "bcd", "cde" }), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?names=abc&names=bcd&names=cde", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_MethodWithListOfInts()
        {
            Test<ICustomerApi>(api => api.MethodWithIntegerListParameter(new List<int> { 1, 2, 3 }), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?values=1&values=2&values=3", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_MethodWithListAndDefaultReverseResolverForStrings()
        {
            Test<ICustomerApi>(
                api => api.MethodWithListParameter(new List<string> { "abc", "bcd", "cde" }),
                m =>
                    {
                        Assert.AreEqual(HttpMethod.Get, m.Method);
                        Assert.AreEqual("api/customer?names=cba&names=dcb&names=edc", m.RequestUri.ToString());
                    },
            x => x.DefaultParameterListTransformers.Add(new ReverseAttribute()));
        }

        [Test]
        public void UrlWithQueryParams_ReverseParameterOrder()
        {
            Test<ICustomerApi>(api => api.MethodWithListParameter(new List<string> { "abc", "bcd", "cde" }),
                m =>
                {
                    Assert.AreEqual(HttpMethod.Get, m.Method);
                    Assert.AreEqual("api/customer?names=cde&names=bcd&names=abc", m.RequestUri.ToString());
                },
                x => x.DefaultParameterListTransformers.Add(new ReverseParameterListTransformers()));
        }

        [Test]
        public void UrlWithQueryParams_AddExtraParameter()
        {
            Test<ICustomerApi>(api => api.GetCustomers("test"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?filter=test&extra=3", m.RequestUri.ToString());
            },
            x => x.DefaultParameterListTransformers.Add(new AddExtraParameterTransformer("extra", 3)));
        }

        [Test]
        public void UrlWithQueryParams_QueryStringParametersAreUrlEncoded()
        {
            Test<ICustomerApi>(api => api.GetCustomers("my filter?"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer?filter=my+filter%3F", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_RouteSegmentParametersAreUrlEncoded()
        {
            Test<ICustomerApi>(api => api.GetCustomers6("my resource"), m =>
            {
                Assert.AreEqual(HttpMethod.Get, m.Method);
                Assert.AreEqual("api/customer/my+resource", m.RequestUri.ToString());
            });
        }

        [Test]
        public void UrlWithQueryParams_CultureSensitiveParametersAreInvariantlyTransformed()
        {
            var expectedResult = WebUtility.UrlEncode("03/07/2014 00:00:00");
            RunWithCulture("en-US", () => Test<ICustomerApi>(api => api.GetCustomers(new DateTime(2014, 03, 07)), m =>
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
