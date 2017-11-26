namespace WebAnchor.RequestFactory.Transformation
{
    public class RequestTransformContext
    {
        public RequestTransformContext(ApiInvocation apiInvocation)
        {
            ApiInvocation = apiInvocation;
        }

        public ApiInvocation ApiInvocation { get; private set; }
        public string UrlTemplate { get; internal set; }
        public IContentSerializer ContentSerializer { get; set; }
    }
}