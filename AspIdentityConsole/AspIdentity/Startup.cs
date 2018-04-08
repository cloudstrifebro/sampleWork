using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AspIdentity.Startup))]
namespace AspIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
