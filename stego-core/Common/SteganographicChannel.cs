using System;

namespace Stego.Core.Common
{
    public class SteganographicChannel
    {
        public string SourceAddress { get; protected set; }
        public BitStream Stream { get; protected set; }
        public ChannelStates State { get; protected set; }


        public SteganographicChannel (string sourceAddress)
        {
            SourceAddress = sourceAddress;
            State = ChannelStates.Waiting;
        }

        public ChannelStates Process (HttpRequestEnvelope request, ISteganographicTechnique technique)
        {
            if (State == ChannelStates.Terminated)
            {
                throw new Exception("A terminated channel cannot be processed");
            }

            BitList decodedBits = technique.Decode (request);
            if (decodedBits.Count != 0)
            {
                if (State == ChannelStates.Waiting)
                {
                    Stream = new BitStream();
                    State = ChannelStates.Ready;
                }

                Stream.Write (decodedBits);

                if (Stream.HasLength && Stream.Length == Stream.ReadLength)
                {
                    State = ChannelStates.Terminated;
                }
            }

            return State;
        }
    }
}