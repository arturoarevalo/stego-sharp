namespace Stego.Core.Common
{
    public class HttpHeaderEnvelope
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public HttpHeaderEnvelope(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}