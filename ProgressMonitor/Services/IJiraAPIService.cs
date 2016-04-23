using System.Collections.Generic;
using ProgressMonitor.Models.JsonSerialization;

namespace ProgressMonitor.Services
{
	public interface IJiraAPIService
	{
		IReadOnlyCollection<JiraProject> GetAllProjects();

		JiraProject GetProject(long id);

		IReadOnlyCollection<JiraIssue> GetIssuesByVersion(long versionId);
	}
}