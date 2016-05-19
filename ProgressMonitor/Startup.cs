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
			// CODE FIRST MIGRATIONS
			#if !DEBUG
			var migrator = new DbMigrator(new Configuration());
			migrator.Update();
			#endif
		}
	}
}
