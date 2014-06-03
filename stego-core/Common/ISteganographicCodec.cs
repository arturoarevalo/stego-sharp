namespace Stego.Core.Common
{
    public interface ISteganographicCodec
    {
        string Encode (BitStream stream, string data);

        int Decode (string data, BitStream stream);
    }
}