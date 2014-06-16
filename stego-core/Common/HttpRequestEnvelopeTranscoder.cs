using System;
using System.Net;
using System.Web;

namespace Stego.Core.Common
{
    using Stego.Core.Extensions;

    public static class HttpRequestEnvelopeTranscoder
    {
        public static HttpWebRequest Transcode (HttpRequestEnvelope request)
        {
            var result = (HttpWebRequest) WebRequest.Create (request.Url);

            result.Method = request.Method;

            if (request.Cookies.Count > 0)
            {
                // copy cookies
                result.CookieContainer = new CookieContainer ();
                foreach (var cookie in request.Cookies)
                {
                    result.CookieContainer.Add (new Cookie()
                                                {
                                                    Name = cookie.Name,
                                                    Value = cookie.Value,
                                                    Domain = "localhost"
                                                });
                }
            }

            // copy headers
            foreach (var header in request.Headers)
            {
                result.SetRawHeader (header.Name, header.Value);
            }

            return result;
        }

        public static HttpRequestEnvelope Transcode (HttpRequest request)
        {
            var result = new HttpRequestEnvelope
                         {
                             Method = request.HttpMethod, 
                             Address = request.UserHostAddress
                         };

            // copy cookies
            foreach (var key in request.Cookies.AllKeys)
            {
                var cookie = request.Cookies [key];
                result.Cookies.Add (new HttpCookieEnvelope (cookie.Name, cookie.Value));
            }

            // copy headers
            foreach (var key in request.Headers.AllKeys)
            {
                var header = request.Headers [key];
                result.Headers.Add(new HttpHeaderEnvelope (key, header));
            }

            return result;
        }
    }
}