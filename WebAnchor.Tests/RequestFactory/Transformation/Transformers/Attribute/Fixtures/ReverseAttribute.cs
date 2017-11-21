﻿using System;
using System.Collections.Generic;
using System.Linq;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.RequestFactory.Transformation.Transformers.Attribute;

namespace WebAnchor.Tests.RequestFactory.Transformation.Transformers.Attribute.Fixtures
{
    public class ReverseAttribute : ParameterTransformerAttribute, IParameterListTransformer
    {
        public bool CanResolve(Parameter parameter)
        {
            return parameter.SourceValue is string;
        }

        public override void Apply(Parameter parameter)
        {
            if (CanResolve(parameter))
            {
                parameter.Value = parameter.Value.ToString().Reverse().Aggregate(string.Empty, (x, y) => x + y);
            }
        }

        public IEnumerable<Parameter> TransformParameters(IEnumerable<Parameter> parameters, ParameterTransformContext parameterTransformContext)
        {
            foreach (var parameter in parameters)
            {
                Apply(parameter);
            }

            return parameters;
        }

        public void ValidateApi(Type type)
        {
        }
    }
}