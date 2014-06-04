namespace Stego.Core.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using Stego.Core.Common;
    using Stego.Core.ChannelSavers;

    public class RequestProcessor
    {
        private static RequestProcessor instance;

        private Dictionary <string, TechniqueProcessor> pathMap = new Dictionary <string, TechniqueProcessor> ();
        private Dictionary <ISteganographicTechnique, TechniqueProcessor> techniqueMap = new Dictionary <ISteganographicTechnique, TechniqueProcessor> ();

        public static RequestProcessor Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RequestProcessor ();
                }

                return instance;
            }
        }

        public IChannelSaver Saver { get; set; }

        private RequestProcessor ()
        {
            instance = this;
            Saver = new DebugConsoleChannelSaver ();
        }

        public void Process (HttpRequest request)
        {
            string path = request.Url.AbsolutePath;

            TechniqueProcessor processor;
            if (pathMap.TryGetValue (path, out processor))
            {
                HttpRequestEnvelope envelope = HttpRequestEnvelopeTranscoder.Transcode (request);
                processor.Process (envelope);
            }
        }

        public void Register (String path, ISteganographicTechnique technique)
        {
            // find or create a new processor for this steganigrapic technique
            TechniqueProcessor processor;
            if (!techniqueMap.TryGetValue (technique, out processor))
            {
                processor = new TechniqueProcessor (technique);
                techniqueMap.Add (technique, processor);
            }

            // register the url with a technique processor
            pathMap.Add (path, processor);
        }

        public void Register (IEnumerable <string> paths, ISteganographicTechnique technique)
        {
            foreach (var path in paths)
            {
                Register (path, technique);
            }
        }

    }
}