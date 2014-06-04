using System;
using System.Collections.Generic;
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
    class Program
    {
        static void Main (string [] args)
        {
            var technique = new RefererSubstitution ();
            technique.UrlList.Add ("http://localhost:22000/test1.html");
            //technique.UrlList.Add ("http://www.referer2.com/samplePage.html");
            //technique.Codec = new Base32Codec (128);

            var generator = new RequestGenerator ();
            generator.UrlList.Add ("http://localhost:22000/test1.html");
            generator.Technique = technique;


            foreach (var request in generator.Generate ("Hello world!!!!"))
            {
                var transcoded = HttpRequestEnvelopeTranscoder.Transcode (request);

                Console.WriteLine("Request {0}", request);
                HttpWebResponse response = (HttpWebResponse) transcoded.GetResponse ();
                Console.WriteLine ("   response code {0}", response.StatusCode);
            }

            Console.WriteLine("Done!");

            Console.ReadKey ();
        }
    }
}
