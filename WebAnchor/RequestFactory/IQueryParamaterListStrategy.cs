using System;
using System.Collections.Generic;

namespace WebAnchor.RequestFactory
{
    public interface IQueryParamaterListStrategy
    {
        IEnumerable<Tuple<string, string>> CreateNameValuePairs(Parameter parameter, IEnumerable<string> values);
    }
}