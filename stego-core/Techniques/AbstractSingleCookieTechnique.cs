namespace Stego.Core.Techniques
{
    using System;
    using System.Linq;
    using Stego.Core.Common;

    public abstract class AbstractSingleCookieTechnique : ISteganographicTechnique
    {
        protected string cookieName;

        protected abstract string EncodeValue (BitStream data);
        protected abstract BitList DecodeValue (string data);

        protected AbstractSingleCookieTechnique (string header)
        {
            cookieName = header;
        }

        private AbstractSingleCookieTechnique ()
        {
            // disallow default constructor
        }

        public int Encode (BitStream data, HttpRequestEnvelope request)
        {
            string value = EncodeValue (data);
            request.Cookies.Add (new HttpCookieEnvelope (cookieName, value));

            return 0;
        }

        public BitList Decode (HttpRequestEnvelope request)
        {
            HttpCookieEnvelope cookie = request.Cookies.FirstOrDefault (h => h.Name.Equals (cookieName, StringComparison.InvariantCultureIgnoreCase));
            if (cookie != null)
            {
                if (!String.IsNullOrEmpty (cookie.Value))
                {
                    return DecodeValue (cookie.Value);
                }
            }

            return new BitList ();
        }
    }
}