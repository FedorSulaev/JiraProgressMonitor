using System.Collections.Generic;
using ProgressMonitor.Models.JsonSerialization;

namespace ProgressMonitor.Services
{
	public interface IJiraAPIService
	{
		IReadOnlyList<Project> GetAllProjects();

		Project GetProject(long id);
	}
}