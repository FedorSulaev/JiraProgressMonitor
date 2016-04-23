using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProgressMonitor.Models.DbModels
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Project> ProjectSet { get; set; }

		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
			
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}
	}
}