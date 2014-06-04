
namespace Stego.Core.Techniques
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Stego.Core.Common;

    public class EmptyTechnique : AbstractTechnique
    {
        public override int Encode (BitStream data, HttpRequestEnvelope request)
        {
            throw new NotImplementedException ();
        }

        public override BitList Decode (HttpRequestEnvelope request)
        {
            throw new NotImplementedException ();
        }
    }
}