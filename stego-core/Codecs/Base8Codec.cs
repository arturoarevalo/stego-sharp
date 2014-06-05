namespace Stego.Core.Codecs
{
    public class Base8Codec : GenericBaseCodec
    {
        private const string Base8CharacterMap = "01234567";

        public Base8Codec (int minumum, int maximum, bool allowOverflow = false) : base (3, Base8CharacterMap, minumum, maximum, allowOverflow) { }
        public Base8Codec (int minumum, bool allowOverflow = false) : base (3, Base8CharacterMap, minumum, minumum, allowOverflow) { }
    }
}