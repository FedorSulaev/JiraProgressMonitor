using System.Web.Mvc;
using ProgressMonitor.Services;

namespace ProgressMonitor.Controllers
{
	[Authorize]
    public class MonitorOverviewController : Controller
    {
		private readonly IJiraAPIService _jiraAPIService;

		public MonitorOverviewController(IJiraAPIService jiraAPIService)
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