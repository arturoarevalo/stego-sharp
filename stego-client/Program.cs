using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Stego.Core.Client;
using Stego.Core.Codecs;
using Stego.Core.Common;
using Stego.Core.Techniques;

namespace Stego.Client
{
    internal class Program
    {
        private static void Main (string [] args)
        {
            // check command line parameters
            if (args.Length < 3)
            {
                Console.WriteLine ("Invalid parameters");
                Environment.Exit (1);
            }

            // create the request generator
            var generator = new RequestGenerator ();

            // parse technique name
            switch (args [0].ToLower ())
            {
                case "range-16":
                    generator.Technique = new RangeHeaderTechnique (16);
                    break;

                case "range-32":
                    generator.Technique = new RangeHeaderTechnique (31);
                    break;

                case "referer":
                    generator.Technique = new RefererSubstitution ();
                    break;

                case "referer-base64-32":
                    generator.Technique = new RefererSubstitution { Codec = new Base64Codec (32) };
                    break;

                case "cookies-google":
                    generator.Technique = new GoogleAnalyticsCookiesTechnique ();
                    break;

                default:
                    Console.WriteLine ("Invalid technique name");
                    Environment.Exit (1);
                    break;
            }


            // load urls
            foreach (var url in args [1].Split (','))
            {
                generator.UrlList.Add (url);
            }

            // load data
            string data = args [2];

            // initialize statistics
            int requestsDone = 0;
            int stegoBits = data.Length * 8 + 8; // 8 bits per character + 8 bits (size)
            int additionalBits = 0;

            foreach (var request in generator.Generate (data))
            {
                // update statistics
                additionalBits += (request.FinalSize - request.OriginalSize) * 8;
                requestsDone++;

                var transcoded = HttpRequestEnvelopeTranscoder.Transcode (request);

                Console.WriteLine ("Performing request {0}", request);
                HttpWebResponse response = (HttpWebResponse) transcoded.GetResponse ();
                Console.WriteLine ("   response code {0}", response.StatusCode);
            }

            Console.WriteLine ("");
            Console.WriteLine ("-- Statistics --");
            Console.WriteLine ("Stego message: {0} bits", stegoBits);
            Console.WriteLine ("Requests done: {0}", requestsDone);
            Console.WriteLine ("Additional data sent: {0} bits", additionalBits);
            Console.WriteLine ("Stego bits/request: {0} bpr", stegoBits / requestsDone);
            Console.WriteLine ("Additional bits/request: {0} bpr", additionalBits / requestsDone);
        }
    }
}