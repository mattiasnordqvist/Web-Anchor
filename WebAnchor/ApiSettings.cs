using System.Collections.Generic;
using Newtonsoft.Json;

using WebAnchor.RequestFactory;
using WebAnchor.RequestFactory.Transformation;
using WebAnchor.RequestFactory.Transformation.Transformers.Default;
using WebAnchor.ResponseParser;

namespace WebAnchor
{
    public class ApiSettings : ISettings
    {
        public ApiSettings()
        {
            ParameterListTransformers = new DefaultParameterListTransformers();
            ResponseHandlers = new DefaultResponseHandlers();
            ContentSerializer = new ContentSerializer(new JsonSerializer());
            InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl = true;
            PreservePathInUrlSegmentParameters = true;
        }

        public virtual List<IParameterListTransformer> ParameterListTransformers { get; set; }
        public virtual List<IResponseHandler> ResponseHandlers { get; set; }
        public virtual IContentSerializer ContentSerializer { get; set; }

        public virtual bool InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl { get; set; }
        public virtual bool PreservePathInUrlSegmentParameters { get; set; }

        public IHttpRequestFactory GetRequestFactory()
        {
            var requestFactory = new HttpRequestFactory(ContentSerializer, ParameterListTransformers)
            {
                InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl = InsertMissingSlashBetweenBaseLocationAndVerbAttributeUrl,
                TreatUrlSegmentSeparatorsInUrlSegmentSubstitutionsAsUrlSegmentSeparators = PreservePathInUrlSegmentParameters
            };
            return requestFactory;
        }

        public IHttpResponseParser GetResponseParser()
        {
            return new HttpResponseParser(ResponseHandlers);
        }
    }
}