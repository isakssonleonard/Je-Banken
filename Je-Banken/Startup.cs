using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Je_Banken.Startup))]
namespace Je_Banken
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
