using System;

using Nancy.Hosting.Self;

using NUnit.Framework;

namespace WebAnchor.Tests.TestUtils
{
    public class IntegrationTest
    {
        protected const string Host = "http://localhost:1111/";
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
    }
}