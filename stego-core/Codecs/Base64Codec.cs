namespace Stego.Core.Codecs
{
    public class Base64Codec : GenericBaseCodec
    {
        private const string Base64CharacterMap = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

        public Base64Codec (int minumum, int maximum) : base (6, Base64CharacterMap, minumum, maximum) { }
        public Base64Codec (int minumum) : base (6, Base64CharacterMap, minumum, minumum) { }
    }
}