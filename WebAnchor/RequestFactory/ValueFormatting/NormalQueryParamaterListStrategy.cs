using System;
using System.Collections.Generic;

namespace WebAnchor.RequestFactory.ValueFormatting
{
    public class NormalQueryParamaterListStrategy : IQueryParamaterListStrategy
    {
        public NormalQueryParamaterListStrategy()
        {
        }

        public IEnumerable<Tuple<string, string>> CreateNameValuePairs(Parameter parameter, IEnumerable<string> values)
        {
            foreach (var value in values)
            {
                yield return Tuple.Create(parameter.Name, value);
            }
        }
    }
}