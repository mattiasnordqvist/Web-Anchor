using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebAnchor.Attributes.Content;

namespace WebAnchor.RequestFactory.Transformation.Transformers
{
    public class ParameterCreatorTransformer : IParameterListTransformer
    {
        public RequestTransformContext Context { get; set; }

        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, RequestTransformContext parameterTransformContext)
        {
            Context = parameterTransformContext;
            return
                Context.MethodInfo.GetParameters()
                   .Select((x, i) => new { Index = i, ParameterInfo = x })
                   .Where(x => Context.ApiInvocation.GetArgumentValue(x.Index) != null)
                   .Select(x => new Parameter(x.ParameterInfo, Context.ApiInvocation.GetArgumentValue(x.Index), ResolveParameterType(x.ParameterInfo, parameterTransformContext.UrlTemplate)))
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