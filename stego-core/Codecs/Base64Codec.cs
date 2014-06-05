namespace Stego.Core.Codecs
{
    public class Base64Codec : GenericBaseCodec
    {
        private const string Base64CharacterMap = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

        public Base64Codec (int minumum, int maximum, bool allowOverflow = false) : base (6, Base64CharacterMap, minumum, maximum, allowOverflow) { }
        public Base64Codec (int minumum, bool allowOverflow = false) : base (6, Base64CharacterMap, minumum, minumum, allowOverflow) { }
    }
}