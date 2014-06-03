using System.Runtime.ExceptionServices;
using Stego.Core.Common;

namespace Stego.Core.Codecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


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

        public int Decode (string data, BitStream stream)
        {
            if (String.IsNullOrEmpty (data))
            {
                return 0;
            }

            int done = 0;
            for (int i = 0; i < data.Length; i++)
            {
                int index = CharacterMap.IndexOf (data [i]);
                if (index != -1)
                {
                    stream.Write (index, BitsPerCharacter);
                    done += BitsPerCharacter;
                }
            }

            return done;
        }
    }
    

    public class Base16Codec : GenericBaseCodec
    {
        private const string Base16CharacterMap = "0123456789abcdef";

        public Base16Codec (int minumum, int maximum) : base (4, Base16CharacterMap, minumum, maximum) { }
        public Base16Codec (int minumum) : base (4, Base16CharacterMap, minumum, minumum) { }
    }

    public class Base32Codec : GenericBaseCodec
    {
        private const string Base32CharacterMap = "abcdefghijklmnopqrstuvwxyz234567";

        public Base32Codec (int minumum, int maximum) : base (5, Base32CharacterMap, minumum, maximum) { }
        public Base32Codec (int minumum) : base (5, Base32CharacterMap, minumum, minumum) { }
    }

    class StringCapitalizationCodec : ISteganographicCodec
    {
        private int maximumReplacements = 0;

        public string Encode (BitStream stream, string data)
        {
            if (String.IsNullOrEmpty (data))
            {
                return data;
            }

            StringBuilder builder = new StringBuilder();

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

        public int Decode (string data, BitStream stream)
        {
            if (String.IsNullOrEmpty (data))
            {
                return 0;
            }

            int done = 0;
            for (int i = 0; i < data.Length; i++)
            {
                char original = data [i];

                bool valid = Char.IsLetter (original);
                if (valid && (done < maximumReplacements || maximumReplacements == 0))
                {
                    stream.Write (Char.IsUpper (original));
                    done++;
                }
            }

            return done;
        }
    }
}