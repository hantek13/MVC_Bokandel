using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Bokhandel.Startup))]
namespace MVC_Bokhandel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
