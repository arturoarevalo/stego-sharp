namespace Stego.Core.Techniques
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Stego.Core.Common;

    public abstract class AbstractMultipleCookieTechnique : ISteganographicTechnique
    {
        protected List <string> cookieNames = new List <string> ();

        public AbstractMultipleCookieTechnique (IEnumerable <string> cookieNames)
        {
            foreach (var cookieName in cookieNames)
            {
                this.cookieNames.Add (cookieName);
            }
        }

        private AbstractMultipleCookieTechnique ()
        {
            // disallow default constructor
        }

        public int Encode (BitStream data, HttpRequestEnvelope request)
        {
            foreach (var cookieName in cookieNames)
            {
                string value = EncodeValue (data, cookieName);
                request.Cookies.Add (new HttpCookieEnvelope (cookieName, value));
            }

            return 0;
        }

        public BitList Decode (HttpRequestEnvelope request)
        {
            BitList stream = new BitList ();

            foreach (var cookieName in cookieNames)
            {
                HttpCookieEnvelope cookie = request.Cookies.FirstOrDefault (h => h.Name.Equals (cookieName, StringComparison.InvariantCultureIgnoreCase));
                if (cookie != null)
                {
                    if (!String.IsNullOrEmpty (cookie.Value))
                    {
                        stream.Add (DecodeValue (cookieName, cookie.Value));
                    }
                }
            }

            return stream;
        }

        protected abstract string EncodeValue (BitStream data, string cookieName);
        protected abstract BitList DecodeValue (string cookieName, string data);
    }
}