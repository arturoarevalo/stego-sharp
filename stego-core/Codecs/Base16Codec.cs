namespace Stego.Core.Codecs
{

    public class Base16Codec : GenericBaseCodec
    {
        private const string Base16CharacterMap = "0123456789abcdef";

        public Base16Codec (int minumum, int maximum, bool allowOverflow = false) : base (4, Base16CharacterMap, minumum, maximum, allowOverflow) { }
        public Base16Codec (int minumum, bool allowOverflow = false) : base (4, Base16CharacterMap, minumum, minumum, allowOverflow) { }
    }
}