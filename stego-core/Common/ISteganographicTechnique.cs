namespace Stego.Core.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    /// <summary>
    /// The base interface for any steganographic technique
    /// </summary>
    public interface ISteganographicTechnique
    {
        /// <summary>
        /// Gets the maximum number of bytes that can be sent / received using this steganographic technique.
        /// </summary>
        int MaximumLengthPerRequest { get; }

        /// <summary>
        /// Encodes the given byte array into a HTTP request.
        /// </summary>
        /// <param name="data">The data to encode, up to MaximumLengthPerRequest bytes.</param>
        /// <param name="request">The HttpRequest object in which to encode the data.</param>
        /// <returns>The number of bytes encoded</returns>
        int Encode (byte [] data, HttpRequest request);

        /// <summary>
        /// Decodes up to MaximumLengthPerRequest bytes from the given HTTP request.
        /// </summary>
        /// <param name="request">The HttpRequest object from which to decode the data.</param>
        /// <returns>A byte array of up to MaximumLengthPerRequest bytes, or null if no data could be decoded.</returns>
        byte [] Decode (HttpRequest request);
    }
}