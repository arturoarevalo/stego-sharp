namespace Stego.Core.Techniques
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Stego.Core.Common;

    public abstract class AbstractTechnique : ISteganographicTechnique
    {
        public abstract int Encode (BitStream data, HttpRequestEnvelope request);

        public abstract BitList Decode (HttpRequestEnvelope request);
    }
}