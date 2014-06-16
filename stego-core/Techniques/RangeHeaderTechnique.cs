namespace Stego.Core.Techniques
{
    using System;
    using Stego.Core.Common;

    public class RangeHeaderTechnique : AbstractSingleHeaderTechnique
    {
        protected int bitsPerValue;

        public RangeHeaderTechnique (int bitsPerValue) : base ("Range")
        {
            this.bitsPerValue = bitsPerValue;
        }

        protected override string EncodeValue (BitStream data, string previousData, HttpRequestEnvelope request)
        {
            int firstValue = data.ReadInt(bitsPerValue);

            return String.Format("bytes={0}-", firstValue);
        }

        protected override BitList DecodeValue (string data, HttpRequestEnvelope request)
        {
            BitList stream = new BitList();

            string [] parts = data.Split ('=');
            if (parts.Length == 2)
            {
                parts = parts [1].Split ('-');

                int firstValue;

                if (Int32.TryParse (parts [0], out firstValue))
                {
                    stream.Add (firstValue, bitsPerValue);
                }
            }

            return stream;
        }
    }
}