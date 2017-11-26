﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebAnchor.Attributes.URL;

namespace WebAnchor.Tests.RequestFactory.Transformation.Custom
{
    [BaseLocation("api/customer")]
    public interface ICustomerApi
    {
        [Get("")]
        Task<HttpResponseMessage> GetCustomers(string filter = null);

        [Get("")]
        Task<HttpResponseMessage> MethodWithListParameter(List<string> names);
    }
}
