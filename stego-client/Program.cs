using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stego.Core.Client;
using Stego.Core.Codecs;
using Stego.Core.Common;
using Stego.Core.Techniques;

namespace stego_client
{
    class Program
    {
        static void Main (string [] args)
        {
            var technique = new RefererSubstitution ();
            technique.UrlList.Add ("http://www.referer1.com/ThisIsThePageName.aspx");
            technique.UrlList.Add ("http://www.referer2.com/samplePage.html");
            //technique.Codec = new Base32Codec (128);

            var generator = new RequestGenerator ();
            generator.UrlList.Add ("http://www.google.es");
            generator.Technique = technique;


            BitStream stream = new BitStream();

            foreach (var request in generator.Generate ("Hello world!!!!"))
            {
                technique.Decode (request, stream);
            }

            byte [] data = stream.ToArray ();

            string s = System.Text.Encoding.UTF8.GetString (data);
            Console.WriteLine(s);
            Console.ReadKey ();
        }
    }
}
