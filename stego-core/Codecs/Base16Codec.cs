namespace Stego.Core.Codecs
{
    public class Base16Codec : GenericBaseCodec
    {
        private const string Base16CharacterMap = "0123456789abcdef";

        public Base16Codec (int minumum, int maximum) : base (4, Base16CharacterMap, minumum, maximum) { }
        public Base16Codec (int minumum) : base (4, Base16CharacterMap, minumum, minumum) { }
    }
}