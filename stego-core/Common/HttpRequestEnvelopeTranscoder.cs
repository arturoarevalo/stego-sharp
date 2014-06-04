using System;
using System.Net;
using System.Web;

namespace Stego.Core.Common
{
    public static class HttpRequestEnvelopeTranscoder
    {


        public static HttpWebRequest Transcode (HttpRequestEnvelope request)
        {
            var result = (HttpWebRequest) WebRequest.Create (request.Url);

            result.Method = request.Method;

            if (!String.IsNullOrEmpty (request.Referer))
            {
                result.Referer = request.Referer;
            }

            if (request.Cookies.Count > 0)
            {
                // copy cookies
                result.CookieContainer = new CookieContainer ();
                foreach (var cookie in request.Cookies)
                {
                    result.CookieContainer.Add (new Cookie()
                                                {
                                                    Name = cookie.Name,
                                                    Value = cookie.Value
                                                });
                }
            }

            return result;
        }

        public static HttpRequestEnvelope Transcode (HttpRequest request)
        {
            var result = new HttpRequestEnvelope
                         {
                             Method = request.HttpMethod, 
                             Address = request.UserHostAddress,
                             Referer = request.Headers["Referer"]
                         };

            // copy cookies
            foreach (var key in request.Cookies.AllKeys)
            {
                var cookie = request.Cookies [key];
                result.Cookies.Add (new HttpCookieEnvelope (cookie.Name, cookie.Value));
            }

            return result;
        }

        
    }
}