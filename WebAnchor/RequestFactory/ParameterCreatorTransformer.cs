using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Castle.Core.Internal;

namespace WebAnchor.RequestFactory
{
    public class ParameterCreatorTransformer : IParameterListTransformer
    {
        public ParameterTransformContext Context { get; set; }

        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            this.Context = parameterTransformContext;
            var url = Context.MethodInfo.GetCustomAttribute<HttpAttribute>().URL;
            return
                Context.MethodInfo.GetParameters()
                   .Select((x, i) => new { Index = i, ParameterInfo = x })
                   .Where(x => Context.ApiInvocation.GetArgumentValue(x.Index) != null)
                   .Select(x => new Parameter(x.ParameterInfo, Context.ApiInvocation.GetArgumentValue(x.Index), ResolveParameterType(x.ParameterInfo, url)))
                   .ToList();
        }

        protected virtual ParameterType ResolveParameterType(ParameterInfo parameterInfo, string url)
        {
            return parameterInfo.HasAttribute<ContentAttribute>()
                       ? ParameterType.Content
                       : (url.Contains(CreateRouteSegmentId(parameterInfo.Name))
                            ? ParameterType.Route
                            : ParameterType.Query);
        }

        public virtual string CreateRouteSegmentId(string name)
        {
            return "{" + name + "}";
        }
    }
}