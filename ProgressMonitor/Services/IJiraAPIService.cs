using System.Collections.Generic;
using ProgressMonitor.Models.JsonSerialization;

namespace ProgressMonitor.Services
{
	public interface IJiraAPIService
	{
		IReadOnlyCollection<JiraProject> GetAllProjects();
		IReadOnlyCollection<JiraProject> GetProjectsByUserId(string userId);

		JiraProject GetProject(long id);

		IReadOnlyCollection<JiraIssue> GetIssuesByVersion(long versionId);
	}
}