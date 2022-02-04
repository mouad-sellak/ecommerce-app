using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute("EcommerceApp",typeof(EcommerceApp.Startup))]
namespace EcommerceApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
