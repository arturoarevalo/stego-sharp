namespace Stego.Core.Techniques
{
    using System;
    using System.Linq;
    using Stego.Core.Common;

    public abstract class AbstractSingleHeaderTechnique : ISteganographicTechnique
    {
        protected string headerName;

        protected abstract string EncodeValue (BitStream data, string previousValue, HttpRequestEnvelope request);
        protected abstract BitList DecodeValue (string data, HttpRequestEnvelope request);

        protected AbstractSingleHeaderTechnique (string header)
        {
            headerName = header;
        }

        private AbstractSingleHeaderTechnique ()
        {
            // disallow default constructor
        }

        public int Encode (BitStream data, HttpRequestEnvelope request)
        {
            string previousValue = null;
            HttpHeaderEnvelope header = request.Headers.FirstOrDefault (h => h.Name.Equals (headerName, StringComparison.InvariantCultureIgnoreCase));
            if (header == null)
            {
                header = new HttpHeaderEnvelope (headerName, "");
                request.Headers.Add (header);
            }

            header.Value = EncodeValue (data, header.Value, request);
            
            return 0;
        }

        public BitList Decode (HttpRequestEnvelope request)
        {
            HttpHeaderEnvelope header = request.Headers.FirstOrDefault (h => h.Name.Equals (headerName, StringComparison.InvariantCultureIgnoreCase));
            if (header != null)
            {
                if (!String.IsNullOrEmpty (header.Value))
                {
                    return DecodeValue (header.Value, request);
                }
            }

            return new BitList ();
        }
    }
}