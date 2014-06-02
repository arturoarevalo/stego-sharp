using System.Net;
using Stego.Core.Common;

namespace Stego.Core.Techniques
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    public class EmptyTechnique : AbstractTechnique
    {
        public override int Encode (BitStream data, HttpWebRequest request)
        {
            throw new NotImplementedException ();
        }

        public override int Decode (HttpRequest request, BitStream data)
        {
            throw new NotImplementedException ();
        }
    }
}