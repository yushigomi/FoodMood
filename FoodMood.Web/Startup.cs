using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FoodMood.Web.Startup))]
namespace FoodMood.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
