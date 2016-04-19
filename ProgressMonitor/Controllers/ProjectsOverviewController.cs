using System.Web.Mvc;
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
	        var model = _jiraAPIService.GetAllProjects();
            return View(model);
        }
    }
}