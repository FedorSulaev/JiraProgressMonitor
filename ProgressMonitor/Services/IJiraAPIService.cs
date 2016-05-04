using System.Collections.Generic;
using ProgressMonitor.Models.JsonSerialization;

namespace ProgressMonitor.Services
{
	public interface IJiraAPIService
	{
		IReadOnlyCollection<JiraProject> GetAllProjects();
		IReadOnlyDictionary<JiraProject, bool> GetProjectAccessForUser(string userId);

		JiraProject GetProject(long id);

		IReadOnlyCollection<JiraIssue> GetIssuesByVersion(long versionId);
	}
}