namespace Stego.Core.Codecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Runtime.ExceptionServices;
    using Stego.Core.Common;

    internal class StringCapitalizationCodec : ISteganographicCodec
    {
        private int maximumReplacements = 0;

        public string Encode (BitStream stream, string data)
        {
            if (String.IsNullOrEmpty (data))
            {
                return data;
            }

            StringBuilder builder = new StringBuilder ();

            string lowerString = data.ToLower ();
            int done = 0;

            for (int i = 0; i < lowerString.Length; i++)
            {
                char original = lowerString [i];

                bool valid = Char.IsLetter (original);

                if (valid && (done < maximumReplacements || maximumReplacements == 0))
                {
                    if (stream.Read ())
                    {
                        original = Char.ToUpper (original);
                        done++;
                    }
                }

                builder.Append (original);
            }

            return builder.ToString ();
        }

        public BitList Decode (string data)
        {
            BitList stream = new BitList ();

            if (!String.IsNullOrEmpty (data))
            {
                int done = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    char original = data [i];

                    bool valid = Char.IsLetter (original);
                    if (valid && (done < maximumReplacements || maximumReplacements == 0))
                    {
                        stream.Add (Char.IsUpper (original));
                        done++;
                    }
                }
            }

            return stream;
        }
    }
}