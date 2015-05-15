﻿using System.Net.Http;
using System.Threading.Tasks;

namespace WebAnchor.Tests.Validation
{
    public interface IApiWithTaskOfHttpResponseMessage
    {
        Task<HttpResponseMessage> TaskOfHttpResponseMessage();
    }
}