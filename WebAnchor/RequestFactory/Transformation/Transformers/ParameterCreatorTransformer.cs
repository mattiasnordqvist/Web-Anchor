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
                   .Select((x, i) => new { ParameterInfo = x, ArgumentValue = requestTransformContext.ApiInvocation.GetArgumentValue(i) })
                   .Where(x => x.ArgumentValue != null)
                   .Select(x => new Parameter(x.ParameterInfo, x.ArgumentValue, ResolveParameterType(x.ParameterInfo, requestTransformContext.UrlTemplate)))
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