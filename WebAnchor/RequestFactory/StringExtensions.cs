using System.Collections.Generic;
using System.Linq;

namespace WebAnchor.RequestFactory
{
    internal static class StringExtensions
    {
        internal static string Replace(this string @this, IDictionary<string, string> replacements)
        {
            return replacements.ToList().Aggregate(@this,
                (x, y) => x.Replace(y.Key, y.Value));
        }

        internal static string CleanUpUrlString(this string @this)
        {
            return @this.Replace("//", "/").Replace("/?", "?").TrimEnd('/');
        }
    }
}