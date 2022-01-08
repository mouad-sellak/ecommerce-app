using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LocationVoiture.Startup))]
namespace LocationVoiture
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
