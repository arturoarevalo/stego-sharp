
namespace Stego.Core.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Stego.Core.Common;

    public class RequestGenerator
    {
        public UrlList UrlList { get; private set; }

        public ISteganographicTechnique Technique { get; set; }

        public IUrlSelector UrlSelector { get; set; }

        public RequestGenerator ()
        {
            UrlList = new UrlList ();
            UrlSelector = new SimpleUrlSelector ();
        }


        public IEnumerable <HttpRequestEnvelope> Generate (string data)
        {
            if (UrlList.Count == 0)
            {
                throw new Exception("UrlList cannot be empty");
            }

            if (Technique == null)
            {
                throw new Exception("Technique cannot be null");
            }

            if (UrlSelector == null)
            {
                throw new Exception("UrlSelector cannot be null");
            }

            BitStream input = new BitStream(data);

            while (input.Remaining > 0)
            {
                HttpRequestEnvelope envelope = new HttpRequestEnvelope ();
                envelope.Url = UrlSelector.SelectNext (UrlList);

                int read = Technique.Encode (input, envelope);

                yield return envelope;
            }
        }

    }
}