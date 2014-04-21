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
        
        private static RequestInterceptor instance;

        /// <summary>
        /// Returns the singleton instance of the class.
        /// </summary>
        public static RequestInterceptor Instance
        {
            get { return instance; }
        }

        public RequestInterceptor ()
        {
            // singleton implementation ... very weak, but will work for now
            instance = this;
        }

        public void Init (HttpApplication context)
        {
            throw new NotImplementedException ();
        }

        public void Dispose ()
        {
        }
    }
}