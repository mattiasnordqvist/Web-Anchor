using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebAnchor.Attributes.Content;
using WebAnchor.Attributes.Header;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Default
{
    public class ParameterCreatorTransformer : IParameterListTransformer
    {
        public IEnumerable<Parameter> Apply(IEnumerable<Parameter> parameters, RequestTransformContext requestTransformContext)
        {
            return
                requestTransformContext.ApiInvocation.Method.GetParameters()
                   .Select((x, i) => new { ParameterInfo = x, ArgumentValue = requestTransformContext.ApiInvocation.GetArgumentValue(i) })
                   .Where(x => x.ArgumentValue != null)
                   .Select(x => ResolveParameter(x.ParameterInfo, x.ArgumentValue, requestTransformContext))
                   .ToList();
        }

        public virtual string CreateRouteSegmentId(string name)
        {
            return "{" + name + "}";
        }

        public void ValidateApi(Type type)
        {
            foreach (var method in type.GetMethods())
            {
                if (method.GetParameters().Count(x => x.GetCustomAttributes(typeof(ContentAttribute), false).Any()) > 1)
                {
                    throw new WebAnchorException($"The method {method.Name} in {type.FullName} cannot have more than one {typeof(ContentAttribute).FullName}");
                }
            }
        }

        protected bool IsEnumerable(object value)
        {
            return value is IEnumerable && (value.GetType().GetTypeInfo().IsGenericType || value.GetType().IsArray);
        }

        protected virtual Parameter ResolveParameter(ParameterInfo parameterInfo, object value, RequestTransformContext requestTransformContext)
        {
            if (parameterInfo.GetCustomAttribute<ContentAttribute>() != null)
            {
                return Parameter.CreateContentParameter(parameterInfo, value);
            }
            else if (parameterInfo.GetCustomAttribute<HeaderAttribute>() != null)
            {
                var values = IsEnumerable(value) ? ((IEnumerable)value).Cast<object>() : new object[] { value };
                var p = Parameter.CreateHeaderParameter(parameterInfo, values);
                parameterInfo.GetCustomAttribute<HeaderAttribute>().Apply(p);
                return p;
            }
            else
            {
                if (requestTransformContext.UrlTemplate.Contains(CreateRouteSegmentId(parameterInfo.Name)))
                {
                    return Parameter.CreateRouteParameter(parameterInfo, value);
                }
                else
                {
                    var values = IsEnumerable(value) ? ((IEnumerable)value).Cast<object>() : new object[] { value };
                    return Parameter.CreateQueryParameter(parameterInfo, values);
                }
            }
        }
    }
}