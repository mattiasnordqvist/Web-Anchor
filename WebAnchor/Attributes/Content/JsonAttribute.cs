﻿using System;
using System.Linq;
using System.Reflection;
using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Serialization;
using WebAnchor.RequestFactory.Transformation;

namespace WebAnchor.Attributes.Content
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class JsonAttribute : ParameterTransformerAttribute
    {
        public override void Apply(Parameter parameter, RequestTransformContext requestTransformContext)
        {
            requestTransformContext.ContentSerializer = new JsonContentSerializer(); // TODO: how about the options?
        }

        public override void ValidateApi(Type type, MethodInfo method, ParameterInfo parameter)
        {
            if (!parameter.GetCustomAttributes(typeof(ContentAttribute), false).Any())
            {
                throw new WebAnchorException($"The attribute {nameof(JsonAttribute)} cannot be used on parameter {parameter.Name} because it does not have a {nameof(ContentAttribute)}");
            }
        }
    }
}
