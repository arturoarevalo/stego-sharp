namespace Stego.Core.Codecs
{
    using System;
    using System.Text;
    using Stego.Core.Common;

    public class GenericBaseCodec : ISteganographicCodec
    {
        private Random random = new Random ();

        public int BitsPerCharacter { get; set; }
        public string CharacterMap { get; set; }

        public int MinimumCharacterCount { get; set; }
        public int MaximumCharactercount { get; set; }

        public GenericBaseCodec (int bitsPerCharacter, string characterMap, int minimum, int maximum)
        {
            BitsPerCharacter = bitsPerCharacter;
            CharacterMap = characterMap;
            MinimumCharacterCount = minimum;
            MaximumCharactercount = maximum;
        }

        public GenericBaseCodec (int bitsPerCharacter, string characterMap, int characterCount)
        {
            BitsPerCharacter = bitsPerCharacter;
            CharacterMap = characterMap;
            MinimumCharacterCount = characterCount;
            MaximumCharactercount = characterCount;
        }

        public string Encode (BitStream stream, string data)
        {
            StringBuilder builder = new StringBuilder();

            int count = random.Next (MinimumCharacterCount, MaximumCharactercount + 1);
            for (int i = 0; i < count; i++)
            {
                if (stream.Remaining > 0)
                {
                    int index = stream.ReadByte (BitsPerCharacter);
                    builder.Append (CharacterMap [index]);
                }
            }

            return builder.ToString ();
        }

        public BitList Decode (string data)
        {
            BitList stream = new BitList ();

            if (String.IsNullOrEmpty (data))
            {

                for (int i = 0; i < data.Length; i++)
                {
                    int index = CharacterMap.IndexOf (data [i]);
                    if (index != -1)
                    {
                        stream.Add (index, BitsPerCharacter);
                    }
                }
            }

            return stream;
        }
    }
}