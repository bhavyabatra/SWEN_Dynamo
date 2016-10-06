using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SWEN_Dynamo.Startup))]
namespace SWEN_Dynamo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
