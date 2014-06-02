namespace Stego.Core.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web;

    /// <summary>
    /// The base interface for any steganographic technique
    /// </summary>
    public interface ISteganographicTechnique
    {
        /// <summary>
        /// Reads a bunch of bits from the stream and encodes them in the HTTP web request object.
        /// </summary>
        /// <param name="data">A bit stream containing data to be encoded.</param>
        /// <param name="request">The HttpWebRequest object in which to encode the data.</param>
        /// <returns>The number of bits encoded.</returns>
        int Encode (BitStream data, HttpWebRequest request);

        /// <summary>
        /// Reads a bunch of bits from a HTTP request and writes them in the stream.
        /// </summary>
        /// <param name="request">The HttpRequest object from which to decode the data.</param>
        /// <param name="data">A BitStream where the decoded bits will be written.</param>
        /// <returns>The number of bits decoded.</returns>
        int Decode (HttpRequest request, BitStream data);
    }
}