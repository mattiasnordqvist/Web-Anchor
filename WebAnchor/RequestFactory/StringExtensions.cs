using System.Collections.Generic;
using System.Linq;

namespace WebAnchor.RequestFactory
{
    public static class StringExtensions
    {
        public static string Replace(this string @this, IDictionary<string, string> replacements)
        {
            return replacements.ToList().Aggregate(@this,
                (x, y) => x.Replace(y.Key, y.Value));
        }
    }
}