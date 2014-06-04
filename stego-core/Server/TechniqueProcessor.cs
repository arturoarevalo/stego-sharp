namespace Stego.Core.Server
{
    using System.Collections.Generic;
    using Stego.Core.Common;

    public class TechniqueProcessor
    {
        private Dictionary <string, SteganographicChannel> channels = new Dictionary <string, SteganographicChannel> ();

        public ISteganographicTechnique Technique { get; set; }

        public TechniqueProcessor (ISteganographicTechnique technique)
        {
            Technique = technique;
        }

        public void Process (HttpRequestEnvelope request)
        {
            // find a channel for the request address
            SteganographicChannel channel;
            if (!channels.TryGetValue (request.Address, out channel))
            {
                channel = new SteganographicChannel (request.Address);
                channels.Add (request.Address, channel);
            }

            // perform decoding on the channel
            if (channel.Process (request, Technique) == ChannelStates.Terminated)
            {
                // save channel
                RequestProcessor.Instance.Saver.Save (channel);

                // remove channel 
                channels.Remove (request.Address);
            }
        }
    }
}