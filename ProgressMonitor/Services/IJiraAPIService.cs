using System.Collections.Generic;
using ProgressMonitor.Models.JsonSerialization;

namespace ProgressMonitor.Services
{
	public interface IJiraAPIService
	{
		IReadOnlyCollection<Project> GetAllProjects();

		Project GetProject(long id);

		IReadOnlyCollection<Issue> GetIssuesByVersion(long versionId);
	}
}