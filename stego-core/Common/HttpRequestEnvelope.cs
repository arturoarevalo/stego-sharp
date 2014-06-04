using System;

namespace Stego.Core.Common
{
    using System.Collections.Generic;
    using System.Runtime.Remoting.Messaging;
    using System.Text;

    public class HttpRequestEnvelope
    {
        public string Method { get; set; }
        public string Address { get; set; }
        public string Url { get; set; }
        public string Referer { get; set; }

        public List<HttpCookieEnvelope> Cookies { get; private set; }
        public List<HttpHeaderEnvelope> Headers { get; private set; }


        public HttpRequestEnvelope ()
        {
            Cookies = new List <HttpCookieEnvelope> ();
            Headers = new List <HttpHeaderEnvelope> ();
            Method = "GET";
        }


        public override string ToString ()
        {
            StringBuilder builder = new StringBuilder();

            builder
                .AppendFormat ("Method [{0}]", Method)
                .AppendFormat (" Url [{0}]", Url)
                .AppendFormat (" Referer [{0}]", Referer);

            foreach (var cookie in Cookies)
            {
                builder.AppendFormat (" Cookie [{0}]=[{1}]", cookie.Name, cookie.Value);
            }

            foreach (var header in Headers)
            {
                builder.AppendFormat(" Header [{0}]=[{1}]", header.Name, header.Value);
            }

            return builder.ToString ();
        }
    }
}