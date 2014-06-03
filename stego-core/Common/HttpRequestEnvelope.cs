using System.Collections.Generic;

namespace Stego.Core.Common
{
    public class HttpRequestEnvelope
    {
        public string Url { get; set; }
        public string Referer { get; set; }

        public List<HttpCookieEnvelope> Cookies { get; private set; }

        public HttpRequestEnvelope ()
        {
            Cookies = new List <HttpCookieEnvelope> ();
        }
    }
}