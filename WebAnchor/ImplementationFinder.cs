using System;
using System.Linq;

namespace WebAnchor
{
    public class ImplementationFinder
    {
        public Type FindMatch(Type[] availableTypes, Type matchThis)
        {

            var implementor = availableTypes
                .FirstOrDefault(x => x.IsClass && matchThis.IsAssignableFrom(x));
            if (implementor == null && matchThis.IsConstructedGenericType)
            {
                var candidates = availableTypes
                    .Where(x => x.IsGenericTypeDefinition);

                foreach (var candidate in candidates)
                {
                    try
                    {
                        var goodCandidate = candidate.MakeGenericType(matchThis.GetGenericArguments());
                        if (goodCandidate.IsClass && matchThis.IsAssignableFrom(goodCandidate))
                        {
                            return goodCandidate;
                        }
                    }
                    catch (ArgumentException)
                    { // Catch and swallow. Easiest way... }
                    }

                }

            }
            return implementor;
        }
    }
}
