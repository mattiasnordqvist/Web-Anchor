using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebAnchor.Attributes.Content;

namespace WebAnchor.RequestFactory.Transformation.Transformers
{
    public class ParameterCreatorTransformer : IParameterListTransformer
    {
        public IEnumerable<Parameter> Apply(IEnumerable<Parameter> parameters, RequestTransformContext requestTransformContext)
        {
            return
                requestTransformContext.ApiInvocation.Method.GetParameters()
                   .Select((x, i) => new { Index = i, ParameterInfo = x })
                   .Where(x => requestTransformContext.ApiInvocation.GetArgumentValue(x.Index) != null)
                   .Select(x => new Parameter(x.ParameterInfo, requestTransformContext.ApiInvocation.GetArgumentValue(x.Index), ResolveParameterType(x.ParameterInfo, requestTransformContext.UrlTemplate)))
                   .ToList();
        }

        public virtual string CreateRouteSegmentId(string name)
        {
            return "{" + name + "}";
        }

        public void ValidateApi(Type type)
        {
        }

        protected virtual ParameterType ResolveParameterType(ParameterInfo parameterInfo, string url)
        {
            if (parameterInfo.GetCustomAttribute<ContentAttribute>() != null)
            {
                return ParameterType.Content;
            }

            return url.Contains(CreateRouteSegmentId(parameterInfo.Name))
                            ? ParameterType.Route
                            : ParameterType.Query;
        }
    }
}