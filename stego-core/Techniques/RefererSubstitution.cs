using System;
using System.CodeDom;
using Stego.Core.Client;
using Stego.Core.Codecs;
using Stego.Core.Common;

namespace Stego.Core.Techniques
{

    public class RefererSubstitution : AbstractTechnique
    {
        public UrlList UrlList { get; private set; }
        public IUrlSelector UrlSelector { get; set; }
        public ISteganographicCodec Codec { get; set; }


        public RefererSubstitution ()
        {
            Codec = new StringCapitalizationCodec ();
            UrlList = new UrlList ();
            UrlSelector = new SimpleUrlSelector ();
        }


        public override int Encode (BitStream data, HttpRequestEnvelope request)
        {
            if (String.IsNullOrEmpty (request.Referer))
            {
                request.Referer = UrlSelector.SelectNext (UrlList);
            }

            request.Referer = Codec.Encode (data, request.Referer);

            return 0;
        }

        public override BitList Decode (HttpRequestEnvelope request)
        {
            return Codec.Decode (request.Referer);
        }
    }

}