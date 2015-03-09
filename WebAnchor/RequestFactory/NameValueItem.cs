namespace WebAnchor.RequestFactory
{
    public class NameValueItem
    {
        public NameValueItem(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}