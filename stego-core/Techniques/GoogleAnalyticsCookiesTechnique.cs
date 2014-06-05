using System;
using Stego.Core.Codecs;
using Stego.Core.Common;

namespace Stego.Core.Techniques
{
    public class GoogleAnalyticsCookiesTechnique : AbstractMultipleCookieTechnique
    {
        public GoogleAnalyticsCookiesTechnique () : base (new [] { "__utma", "__utmb", "__utmc", "__utmz" })
        {
        }

        protected override string EncodeValue (BitStream data, string cookieName)
        {
            if (cookieName == "__utma")
            {
                return String.Format ("{0}.{1}.{2}.{3}.{4}.{5}",
                                      new Base8Codec (8, true).Encode (data, null),
                                      new Base8Codec (10, true).Encode (data, null),
                                      new Base8Codec (10, true).Encode (data, null),
                                      new Base8Codec (10, true).Encode (data, null),
                                      new Base8Codec (10, true).Encode (data, null),
                                      new Base8Codec (1, true).Encode (data, null)
                    );
            }

            if (cookieName == "__utmb")
            {
                return String.Format ("{0}.{1}.{2}.{3}",
                                      new Base8Codec (8, true).Encode (data, null),
                                      new Base8Codec (1, true).Encode (data, null),
                                      new Base8Codec (2, true).Encode (data, null),
                                      new Base8Codec (10, true).Encode (data, null)
                    );
            }

            if (cookieName == "__utmc")
            {
                return String.Format ("{0}",
                                      new Base8Codec (8, true).Encode (data, null)
                    );
            }

            if (cookieName == "__utmz")
            {
                return String.Format ("{0}.{1}.{2}.{3}.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%provided)",
                                      new Base8Codec (8, true).Encode (data, null),
                                      new Base8Codec (10, true).Encode (data, null),
                                      new Base8Codec (1, true).Encode (data, null),
                                      new Base8Codec (1, true).Encode (data, null)
                    );
            }

            return String.Empty;
        }

        protected override BitList DecodeValue (string cookieName, string data)
        {
            BitList stream = new BitList ();

            string [] parts = data.Split ('.');

            if (cookieName == "__utma")
            {
                stream.Add (new Base8Codec (8).Decode (parts [0]));
                stream.Add (new Base8Codec (10).Decode (parts [1]));
                stream.Add (new Base8Codec (10).Decode (parts [2]));
                stream.Add (new Base8Codec (10).Decode (parts [3]));
                stream.Add (new Base8Codec (10).Decode (parts [4]));
                stream.Add (new Base8Codec (1).Decode (parts [5]));
            }

            if (cookieName == "_utmb")
            {
                stream.Add (new Base8Codec (8).Decode (parts [0]));
                stream.Add (new Base8Codec (1).Decode (parts [1]));
                stream.Add (new Base8Codec (2).Decode (parts [2]));
                stream.Add (new Base8Codec (10).Decode (parts [3]));
            }

            if (cookieName == "_utmc")
            {
                stream.Add (new Base8Codec (8).Decode (parts [0]));
            }

            if (cookieName == "_utmz")
            {
                stream.Add (new Base8Codec (8).Decode (parts [0]));
                stream.Add (new Base8Codec (10).Decode (parts [1]));
                stream.Add (new Base8Codec (1).Decode (parts [2]));
                stream.Add (new Base8Codec (1).Decode (parts [3]));
            }

            return stream;
        }
    }
}