using System.Web.UI;

namespace Stego.Core.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BitStream
    {
        private readonly List <bool> sizeBits = new List <bool> ();
        private readonly List <bool> dataBits = new List <bool> ();

        public bool HasLength { get; private set; }
        public int Length { get; private set; }

        public BitStream (string data)
        {
            byte [] utf8 = System.Text.Encoding.UTF8.GetBytes (data);

            Write ((byte) (utf8.Length + 1));
            Write (utf8);
        }

        public BitStream ()
        {
            
        }

        public void Write (bool bit)
        {
            if (sizeBits.Count < 8)
            {
                sizeBits.Add (bit);

                if (sizeBits.Count == 8)
                {
                    HasLength = true;
                    Length 
                }
            }
            else
            {
                dataBits.Add (bit);
            }
        }

        public void Write (byte data)
        {
            for (int i = 0; i < 8; i++)
            {
                Write ((data & (1 << i)) != 0);
            }
        }

        public void Write (byte [] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                Write (data [i]);
            }
        }


        private int ReadLength ()
        {
            int length = 0;

            for (int i = 0; i < 8; i++)
            {
                if (sizeBits [i])
                {
                    length |= (1 << i);
                }
            }

            return length;
        }


    }
}