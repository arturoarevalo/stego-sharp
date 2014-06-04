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
        private int readPosition;

        public bool HasLength { get; private set; }
        public int Length { get; private set; }

        public int ReadLength
        {
            get { return sizeBits.Count + dataBits.Count; }
        }

        public int Remaining
        {
            get { return Length - readPosition; }
        }

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
                    Length = ReadByte (sizeBits, 0) * 8;
                }
            }
            else
            {
                if (ReadLength < Length)
                {
                    dataBits.Add (bit);
                }
            }
        }

        public void Write (BitList data)
        {
            foreach (bool value in data)
            {
                Write (value);
            }
        }

        public void Write (byte data)
        {
            Write (data, 8);
        }

        public void Write (int data, int count)
        {
            for (int i = 0; i < count; i++)
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

        public bool Read ()
        {
            if (readPosition >= ReadLength)
            {
                return false;
            }

            bool value;

            if (readPosition < 8)
            {
                value = sizeBits [readPosition];
            }
            else
            {
                value = dataBits [readPosition - 8];
            }

            readPosition++;
            return value;
        }

        public List<bool> ReadBits (int count)
        {
            List<bool> value = new List<bool> ();

            for (int i = 0; i < count; i++)
            {
                value.Add (Read ());
            }

            return value;
        }

        public byte ReadByte (int count)
        {
            List <bool> value = ReadBits (count);

            return ReadByte (value, 0);
        }

        public int ReadInt (int count)
        {
            List <bool> value = ReadBits (count);

            return ReadInt(value, 0);
        }

        public byte [] ToArray ()
        {
            List <byte> value = new List <byte> ();

            if (HasLength && ReadLength == Length)
            {
                for (int i = 0; i < dataBits.Count; i += 8)
                {
                    value.Add (ReadByte (dataBits, i));
                }
            }

            return value.ToArray ();
        }

        private byte ReadByte (List <bool> source, int start)
        {
            byte value = 0;

            for (int i = start, n = 0; i < start + 8; i++, n++)
            {
                if (i < source.Count && source [i])
                {
                    value |= (byte) (1 << n);
                }
            }

            return value;
        }

        private int ReadInt(List<bool> source, int start)
        {
            int value = 0;

            for (int i = start, n = 0; i < start + 31; i++, n++)
            {
                if (i < source.Count && source[i])
                {
                    value |= (int)(1 << n);
                }
            }

            return value;
        }
    }
}