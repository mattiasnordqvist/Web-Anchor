using System;

namespace WebAnchor.RequestFactory
{
    public class PayloadAttribute : Attribute
    {
        public PayloadAttribute(PayloadType type = PayloadType.Json)
        {
            Type = type;
        }

        public PayloadType Type { get; set; }
    }
}
