#if !DEBUG
using System.Configuration;
using System.Data.Entity.Migrations;
using Configuration = ProgressMonitor.Migrations.Configuration;
#endif
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
			if (bool.Parse(ConfigurationManager.AppSettings["MigrateDatabaseToLatestVersion"]))
			{
				var migrator = new DbMigrator(new Configuration());
				migrator.Update();
			}
			#endif
		}
	}
}
