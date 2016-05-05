using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProgressMonitor.Models.DbModels;
using ProgressMonitor.Services;

namespace ProgressMonitor.Controllers
{
	[Authorize]
    public class VersionController : Controller
    {
	    private readonly IJiraAPIService _jiraAPIService;
		private readonly ApplicationDbContext _context;

	    public VersionController(IJiraAPIService jiraAPIService)
	    {
		    _jiraAPIService = jiraAPIService;
			_context = new ApplicationDbContext();
	    }

	    public ActionResult Index(long versionId, long projectId)
	    {
		    string userId = User.Identity.GetUserId();
			if (_context.Users.First(u => u.Id == userId)
				.AccessibleProjects.All(p => p.JiraId != projectId))
			{
				return null;
			}
			var model = _jiraAPIService.GetIssuesByVersion(versionId);
            return View(model);
        }
    }
}