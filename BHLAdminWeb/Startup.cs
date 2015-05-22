using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("ProductionConfiguration", typeof(MOBOT.BHL.AdminWeb.ProductionStartup))]
[assembly: OwinStartup("DevelopmentConfiguration", typeof(MOBOT.BHL.AdminWeb.DevelopmentStartup))]

namespace MOBOT.BHL.AdminWeb
{
    public partial class DevelopmentStartup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureDevelopmentAuth(app);
        }
    }

    public partial class ProductionStartup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureProductionAuth(app);
        }
    }
}