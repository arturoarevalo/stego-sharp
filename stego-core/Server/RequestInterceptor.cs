namespace Stego.Core.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    /// <summary>
    /// Usage:
    /// </summary>
    public class RequestInterceptor : IHttpModule
    {
        /*
            Web.xml configuration: 
          
            <configuration>
              <system.webServer>
                <modules>
                  <add name="StegoSharpRequestInterceptor" type="Stego.Core.Server.RequestInterceptor"/>
                </modules>
              </system.webServer>
            </configuration>
         */

        public void Init (HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;
        }

        private void OnBeginRequest (object sender, EventArgs eventArgs)
        {
            HttpApplication httpApplication = (HttpApplication) sender;
            HttpRequest request = httpApplication.Request;

#if DEBUG
            System.Diagnostics.Debug.WriteLine ("Begin request interception");
#endif

            RequestProcessor.Instance.Process (request);

#if DEBUG
            System.Diagnostics.Debug.WriteLine ("End request interception");
#endif
        }

        public void Dispose ()
        {
        }
    }
}