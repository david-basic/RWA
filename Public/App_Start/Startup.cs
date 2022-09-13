using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(Public.App_Start.Startup))]

namespace Public.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
