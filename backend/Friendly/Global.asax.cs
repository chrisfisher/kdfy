using System.Web.Http;

namespace Friendly
{      
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FriendlyMapperProfile.RegisterMappings();
        }
    }
}
