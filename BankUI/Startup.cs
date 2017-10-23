using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BankUI.Startup))]
namespace BankUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
