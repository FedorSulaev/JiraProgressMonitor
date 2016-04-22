using System.Linq;
using System.Web.Mvc;
using ProgressMonitor.Constants;
using ProgressMonitor.Models;

namespace ProgressMonitor.Controllers
{
	[Authorize(Roles = UserRoles.AdminRole)]
    public class UserManagementController : Controller
    {
		private readonly ApplicationDbContext _context;

		public UserManagementController()
		{
			_context = new ApplicationDbContext();
		}

		public ActionResult Index()
		{
			var model = _context.Users.Select(u => u.UserName).ToList();
            return View(model);
        }
    }
}