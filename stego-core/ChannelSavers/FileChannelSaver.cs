namespace Stego.Core.ChannelSavers
{
    using System;
    using Stego.Core.Common;

    public class FileChannelSaver : AbstractTextChannelSaver
    {
        public string Path { get; set; }

        public FileChannelSaver (string path)
        {
            if (path.EndsWith ("\\"))
            {
                Path = path;
            }
            else
            {
                Path = path + "\\";
            }
        }

        public override void Save (SteganographicChannel channel, string decodedData)
        {
            string ip = channel.SourceAddress.Replace (".", "_");
            string fileName = Path + ip + ".txt";
            string contents = String.Format ("Received message from IP {0} : [{1}]{2}", channel.SourceAddress, decodedData, Environment.NewLine);

            System.IO.File.AppendAllText (fileName, contents);
        }
    }
}