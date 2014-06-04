namespace Stego.Core.Common
{
    using System;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The base interface for any steganographic technique
    /// </summary>
    public interface ISteganographicTechnique
    {
        /// <summary>
        /// Reads a bunch of bits from the stream and encodes them in the HTTP web request object.
        /// </summary>
        /// <param name="data">A bit stream containing data to be encoded.</param>
        /// <param name="request">The HttpRequestWrapper object in which to encode the data.</param>
        /// <returns>The number of bits encoded.</returns>
        int Encode (BitStream data, HttpRequestEnvelope request);

        /// <summary>
        /// Reads a bunch of bits from a HTTP request and writes them in the stream.
        /// </summary>
        /// <param name="request">The HttpRequestWrapper object from which to decode the data.</param>
        /// <returns>The decoded bits.</returns>
        BitList Decode (HttpRequestEnvelope request);
    }
}