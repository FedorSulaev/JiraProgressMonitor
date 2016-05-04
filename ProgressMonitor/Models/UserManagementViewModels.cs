using System.Collections.Generic;
using ProgressMonitor.Models.JsonSerialization;

namespace ProgressMonitor.Models
{
	public class UserViewModel
	{
		public string Username { get; set; }

		public string Id { get; set; }
	}

	public class UserSettingsViewModel
	{
		public string Username { get; set; }

		public string Id { get; set; }

		public IReadOnlyDictionary<JiraProject, bool> ProjectAccess { get; set; }
	}
}