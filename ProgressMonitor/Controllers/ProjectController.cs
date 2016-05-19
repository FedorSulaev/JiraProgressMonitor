using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProgressMonitor.Models.DbModels;
using ProgressMonitor.Services;

namespace ProgressMonitor.Controllers
{
	[Authorize]
    public class ProjectController : Controller
    {
	    private readonly IJiraAPIService _jiraAPIService;
		private readonly ProgressMonitorDbContext _context;

	    public ProjectController(IJiraAPIService jiraAPIService)
	    {
		    _jiraAPIService = jiraAPIService;
			_context = new ProgressMonitorDbContext();
	    }

	    public ActionResult Index(long id)
	    {
		    string userId = User.Identity.GetUserId();
		    if (_context.Users.First(u => u.Id == userId)
				.AccessibleProjects.All(p => p.JiraId != id))
		    {
			    return null;
		    }
		    var model = _jiraAPIService.GetProject(id);
            return View(model);
        }
    }
}