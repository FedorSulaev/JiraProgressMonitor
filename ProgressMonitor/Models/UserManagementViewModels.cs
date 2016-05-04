using System.Collections.Generic;

namespace ProgressMonitor.Models
{
	public class UserViewModel
	{
		public string Username { get; set; }

		public string Id { get; set; }
	}

	public class ProjectAccessViewModel
	{
		public string ProjectName { get; set; }

		public long ProjectId { get; set; }

		public bool CanAccess { get; set; }
	}

	public class UserSettingsViewModel
	{
		public string Username { get; set; }

		public string Id { get; set; }

		public List<ProjectAccessViewModel> ProjectAccess { get; set; }
	}
}