namespace Stego.Core.ChannelSavers
{
    using System;
    using Stego.Core.Common;

    public class ConsoleChannelSaver : AbstractTextChannelSaver
    {
        public override void Save (SteganographicChannel channel, string decodedData)
        {
            Console.WriteLine ("Received message from IP {0} : [{1}]", channel.SourceAddress, decodedData);
        }
    }
}