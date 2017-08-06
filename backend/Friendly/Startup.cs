using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Friendly.Startup))]

namespace Friendly
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
