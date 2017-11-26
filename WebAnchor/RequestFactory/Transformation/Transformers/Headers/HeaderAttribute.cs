﻿using System;
using System.Collections.Generic;
using System.Linq;

using WebAnchor.RequestFactory.Transformation.Transformers.Attribute.List;

namespace WebAnchor.RequestFactory.Transformation.Transformers.Headers
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
    public class HeaderAttribute : ParameterListTransformerAttribute
    {
        public HeaderAttribute(string headerName, string value)
        {
            HeaderName = headerName;
            Value = value;
        }

        public string HeaderName { get; set; }
        public string Value { get; set; }

        public override IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            var p = parameters.ToList();
            p.Add(new Parameter(null, Value, ParameterType.Header)
            {
                Name = HeaderName,
                Value = Value,
            });
            return p;
        }
    }
}