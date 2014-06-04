namespace Stego.Core.Codecs
{
    public class Base32Codec : GenericBaseCodec
    {
        private const string Base32CharacterMap = "abcdefghijklmnopqrstuvwxyz234567";

        public Base32Codec (int minumum, int maximum) : base (5, Base32CharacterMap, minumum, maximum) { }
        public Base32Codec (int minumum) : base (5, Base32CharacterMap, minumum, minumum) { }
    }
}