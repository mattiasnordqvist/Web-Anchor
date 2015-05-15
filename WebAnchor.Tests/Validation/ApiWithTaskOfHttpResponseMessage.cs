﻿using System.Net.Http;
using System.Threading.Tasks;

namespace WebAnchor.Tests.Validation
{
    public interface ApiWithTaskOfHttpResponseMessage
    {
        Task<HttpResponseMessage> TaskOfHttpResponseMessage();
    }
}