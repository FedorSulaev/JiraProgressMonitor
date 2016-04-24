using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProgressMonitor.Constants;
using ProgressMonitor.Models.JsonSerialization;
using ProgressMonitor.Services;

namespace ProgressMonitor.Controllers
{
	[Authorize]
    public class ProjectsOverviewController : Controller
    {
		private readonly IJiraAPIService _jiraAPIService;

		public ProjectsOverviewController(IJiraAPIService jiraAPIService)
		{
			_jiraAPIService = jiraAPIService;
		}

		// GET: MonitorOverview
        public ActionResult Index()
        {
	        IReadOnlyCollection<JiraProject> model;
	        if (User.IsInRole(UserRoles.AdminRole))
	        {
				model = _jiraAPIService.GetAllProjects();
			}
	        else
	        {
		        model = _jiraAPIService.GetProjectsByUserId(User.Identity.GetUserId());
	        }
            return View(model);
        }
    }
}