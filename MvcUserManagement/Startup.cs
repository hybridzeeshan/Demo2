using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcUserManagement.Startup))]
namespace MvcUserManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
