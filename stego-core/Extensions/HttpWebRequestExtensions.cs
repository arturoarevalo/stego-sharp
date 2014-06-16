namespace Stego.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net;
    using System.Reflection;

    public static class HttpWebRequestExtensions
    {
        private static string [] RestrictedHeaders = new string []
                                                     {
                                                         "Accept",
                                                         "Connection",
                                                         "Content-Length",
                                                         "Content-Type",
                                                         "Date",
                                                         "Expect",
                                                         "Host",
                                                         "If-Modified-Since",
                                                         "Range",
                                                         "Referer",
                                                         "Transfer-Encoding",
                                                         "User-Agent",
                                                         "Proxy-Connection"
                                                     };

        private static Dictionary <string, PropertyInfo> HeaderProperties = new Dictionary <string, PropertyInfo> (StringComparer.OrdinalIgnoreCase);

        static HttpWebRequestExtensions ()
        {
            Type type = typeof (HttpWebRequest);
            foreach (string header in RestrictedHeaders)
            {
                string propertyName = header.Replace ("-", "");
                PropertyInfo headerProperty = type.GetProperty (propertyName);
                HeaderProperties [header] = headerProperty;
            }
        }

        public static void SetRawHeader (this HttpWebRequest request, string name, string value)
        {
            if (name.Equals ("range", StringComparison.InvariantCultureIgnoreCase))
            {
                string [] parts = value.Split ('=');
                if (parts.Length == 2)
                {
                    parts = parts [1].Split ('-');

                    int firstValue;

                    if (Int32.TryParse (parts [0], out firstValue))
                    {
                        request.AddRange (firstValue);
                    }

                    return;
                }
            }

            if (HeaderProperties.ContainsKey (name))
            {
                HeaderProperties [name].SetValue (request, value, null);
            }
            else
            {
                request.Headers [name] = value;
            }
        }
    }
}