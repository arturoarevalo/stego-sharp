namespace Stego.Core.Common
{
    public interface ISteganographicCodec
    {
        string Encode (BitStream stream, string data);

        BitList Decode (string data);
    }
}