using System.Web.Mvc;
using ProgressMonitor.Services;

namespace ProgressMonitor.Controllers
{
    public class ProjectController : Controller
    {
	    private readonly IJiraAPIService _jiraAPIService;

	    public ProjectController(IJiraAPIService jiraAPIService)
	    {
		    _jiraAPIService = jiraAPIService;
	    }

	    public ActionResult Index(long id)
	    {
		    var model = _jiraAPIService.GetProject(id);
            return View(model);
        }
    }
}