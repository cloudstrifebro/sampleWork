using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AspIdentityImplementation.Startup))]
namespace AspIdentityImplementation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
