namespace WebAnchor.RequestFactory.UrlNormalization
{
    public class RemoveDuplicateSlashes : IUrlNormalizer
    {
        public string Normalize(string url)
        {
            var length = int.MaxValue;
            while(length > url.Length)
            {
                length = url.Length;
                url = url.Replace("//", "/");
            }
            return url;
        }
    }
}