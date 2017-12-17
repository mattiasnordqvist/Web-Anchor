using System;
using System.Collections.Generic;

namespace WebAnchor.RequestFactory
{
    public class DelimitedQueryParamaterListStrategy : IQueryParamaterListStrategy
    {
        public string Delimiter = ",";

        public IEnumerable<Tuple<string, string>> CreateNameValuePairs(Parameter parameter, IEnumerable<string> values)
        {
            yield return Tuple.Create(parameter.Name, string.Join(Delimiter,  values));
        }
    }
}