using Microsoft.AspNet.SignalR;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRDemo.Service
{
    // Install-Package Microsoft.Owin.Host.SystemWeb
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.Use(async (context, nextMiddleWare) =>
            //{
            //    await context.Response.WriteAsync("Hello in Hub.");
            //});

            // Install-Package Microsoft.AspNet.Cors
            //  app.UseCors(CorsOptions.AllowAll);

            // Intall-Package Microsoft.AspNet.SignalR
            app.MapSignalR("/hubs", new HubConfiguration());
        }
    }
}