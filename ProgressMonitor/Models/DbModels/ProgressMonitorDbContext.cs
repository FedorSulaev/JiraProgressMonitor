using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProgressMonitor.Models.DbModels
{
	public class ProgressMonitorDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Project> ProjectSet { get; set; }

		public ProgressMonitorDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
			
		}

		public static ProgressMonitorDbContext Create()
		{
			return new ProgressMonitorDbContext();
		}
	}
}