namespace Stego.Core.Common
{
    public class HttpCookieEnvelope
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public HttpCookieEnvelope (string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}