namespace Stego.Core.ChannelSavers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Stego.Core.Common;

    public class DebugConsoleChannelSaver : AbstractTextChannelSaver
    {
        public override void Save (SteganographicChannel channel, string decodedData)
        {
            System.Diagnostics.Debug.WriteLine ("Received message from IP {0} : [{1}]", channel.SourceAddress, decodedData);
        }
    }
}