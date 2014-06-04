namespace Stego.Core.Techniques
{
    using System;
    using System.Linq;
    using Stego.Core.Common;

    public abstract class AbstractSingleHeaderTechnique : ISteganographicTechnique
    {
        protected string headerName;

        protected abstract string EncodeValue (BitStream data);
        protected abstract BitList DecodeValue (string data);

        protected AbstractSingleHeaderTechnique (string header)
        {
            headerName = header;
        }

        private AbstractSingleHeaderTechnique()
        { 
            // disallow default constructor
        }

        public int Encode (BitStream data, HttpRequestEnvelope request)
        {
            string value = EncodeValue (data);
            request.Headers.Add(new HttpHeaderEnvelope(headerName, value));

            return 0;
        }

        public BitList Decode (HttpRequestEnvelope request)
        {

            HttpHeaderEnvelope header = request.Headers.FirstOrDefault(h => h.Name.Equals("Range", StringComparison.InvariantCultureIgnoreCase));
            if (header != null)
            {
                if (!String.IsNullOrEmpty (header.Value))
                {
                    return DecodeValue (header.Value);
                }
            }

            return new BitList ();
        }



    }
}