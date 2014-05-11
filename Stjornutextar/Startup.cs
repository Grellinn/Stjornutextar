using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stjornutextar.Startup))]
namespace Stjornutextar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
