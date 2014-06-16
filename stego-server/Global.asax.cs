using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Stego.Core.ChannelSavers;
using Stego.Core.Codecs;
using Stego.Core.Common;
using Stego.Core.Server;
using Stego.Core.Techniques;

namespace Stego.Server
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start (object sender, EventArgs e)
        {
            RequestProcessor.Instance.Saver = new FileChannelSaver ("c:\\tmp\\tmp");

            ISteganographicTechnique technique = new RefererSubstitution ();
            RequestProcessor.Instance.Register ("/referer.html", technique);

            technique = new RefererSubstitution { Codec = new Base64Codec (32) };
            RequestProcessor.Instance.Register ("/referer-base64-32.html", technique);

            technique = new RangeHeaderTechnique (16);
            RequestProcessor.Instance.Register ("/range-16.html", technique);

            technique = new RangeHeaderTechnique (31);
            RequestProcessor.Instance.Register ("/range-32.html", technique);

            technique = new GoogleAnalyticsCookiesTechnique ();
            RequestProcessor.Instance.Register ("/cookies-google.html", technique);
        }

        protected void Session_Start (object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest (object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest (object sender, EventArgs e)
        {

        }

        protected void Application_Error (object sender, EventArgs e)
        {

        }

        protected void Session_End (object sender, EventArgs e)
        {

        }

        protected void Application_End (object sender, EventArgs e)
        {

        }
    }
}