using System.Collections.Generic;

namespace WebAnchor
{
    public class MultipartContentData
    {
        public MultipartContentData()
        {
            Parts = new List<ContentPartBase>();
        }

        public MultipartContentData(ContentPartBase part)
        {
            Parts = new List<ContentPartBase> { part };
        }

        public MultipartContentData(IEnumerable<ContentPartBase> parts)
        {
            Parts = new List<ContentPartBase>(parts);
        }

        public IList<ContentPartBase> Parts { get; }
    }
}