using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FAC.Web.Startup))]
namespace FAC.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
