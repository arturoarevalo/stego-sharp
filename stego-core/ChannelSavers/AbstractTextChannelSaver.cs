namespace Stego.Core.ChannelSavers
{
    using Stego.Core.Common;

    public abstract class AbstractTextChannelSaver : IChannelSaver
    {

        public void Save (SteganographicChannel channel)
        {
            // get a string from the raw UTF8 received data
            byte [] utf8 = channel.Stream.ToArray ();
            string decodedData = System.Text.Encoding.UTF8.GetString (utf8);

            // save decoded string
            Save (channel, decodedData);
        }

        public abstract void Save (SteganographicChannel channel, string decodedData);
    }
}