using System.Web.Mvc;
using ProgressMonitor.Services;

namespace ProgressMonitor.Controllers
{
	[Authorize]
    public class VersionController : Controller
    {
	    private readonly IJiraAPIService _jiraAPIService;

	    public VersionController(IJiraAPIService jiraAPIService)
	    {
		    _jiraAPIService = jiraAPIService;
	    }

	    public ActionResult Index(long id)
	    {
		    var model = _jiraAPIService.GetIssuesByVersion(id);
            return View(model);
        }
    }
}