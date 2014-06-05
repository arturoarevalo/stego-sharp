namespace Stego.Core.Common
{
    using System.Collections.Generic;

    public class BitList : List<bool>
    {
        public void Add (byte data)
        {
            Add (data, 8);
        }

        public void Add (int data, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Add ((data & (1 << i)) != 0);
            }
        }

        public void Add (byte [] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                Add (data [i]);
            }
        }

        public void Add (BitList data)
        {
            foreach (bool value in data)
            {
                Add (value);
            }
        }
    }
}