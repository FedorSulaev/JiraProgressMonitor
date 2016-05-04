using System.Collections.Generic;

namespace ProgressMonitor.Models.DbModels
{
	public class Project
	{
		public long Id { get; set; }

		public long JiraId { get; set; }

		public virtual ICollection<ApplicationUser> UsersWithAccess { get; set; }
	}
}