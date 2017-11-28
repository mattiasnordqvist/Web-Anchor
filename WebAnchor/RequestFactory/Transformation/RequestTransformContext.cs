namespace WebAnchor.RequestFactory.Transformation
{
    public class RequestTransformContext
    {
        public RequestTransformContext(ApiInvocation apiInvocation, IApiSettings settings) 
        {
            ApiInvocation = apiInvocation;
            Settings = settings;
        }

        public ApiInvocation ApiInvocation { get; private set; }
        public string UrlTemplate { get; internal set; }
        public IContentSerializer ContentSerializer { get; set; }
        public IApiSettings Settings { get; set; }
    }
}