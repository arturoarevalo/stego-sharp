using System.Net;

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
        public abstract int Encode (BitStream data, HttpWebRequest request);

        public abstract int Decode (HttpRequest request, BitStream data);
    }
}