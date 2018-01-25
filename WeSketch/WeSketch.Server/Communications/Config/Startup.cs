using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(WeSketch.Server.Communications.Config.Startup))]

namespace WeSketch.Server.Communications.Config
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
