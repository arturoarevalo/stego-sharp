using System;

namespace Stego.Core.Common
{
    using System.Collections.Generic;

    public class HttpRequestEnvelope
    {
        public string Method { get; set; }
        public string Address { get; set; }
        public string Url { get; set; }
        public string Referer { get; set; }

        public List<HttpCookieEnvelope> Cookies { get; private set; }

        public HttpRequestEnvelope ()
        {
            Cookies = new List <HttpCookieEnvelope> ();
            Method = "GET";
        }


        public override string ToString ()
        {
            return String.Format ("Url [{0}] Referer [{1}]", Url, Referer);
        }
    }
}