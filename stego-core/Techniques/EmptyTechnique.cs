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
        public override int MaximumLengthPerRequest
        {
            get { throw new NotImplementedException (); }
        }

        public override int Encode (byte [] data, HttpRequest request)
        {
            throw new NotImplementedException ();
        }

        public override byte [] Decode (HttpRequest request)
        {
            throw new NotImplementedException ();
        }
    }
}