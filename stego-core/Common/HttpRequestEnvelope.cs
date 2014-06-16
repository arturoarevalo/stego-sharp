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

        public int OriginalSize { get; set; }
        public int FinalSize { get; set; }

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
                .AppendFormat (" Url [{0}]", Url);

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

        public int Size
        {
            get
            {
                int size = 0;

                size += Method.Length;
                size += Url.Length;

                foreach (var cookie in Cookies)
                {
                    size += cookie.Name.Length;
                    size += cookie.Value.Length;
                }

                foreach (var header in Headers)
                {
                    size += header.Name.Length;
                    size += header.Value.Length;
                }

                return size;
            }
        }
    }
}