using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ProgressMonitor.Startup))]
namespace ProgressMonitor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
