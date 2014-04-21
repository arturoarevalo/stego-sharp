namespace Stego.Core.Techniques
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using Stego.Core.Common;

    public abstract class AbstractTechnique : ISteganographicTechnique
    {
        public abstract int MaximumLengthPerRequest { get; }

        public abstract int Encode (byte [] data, HttpRequest request);

        public abstract byte [] Decode (HttpRequest request);
    }
}