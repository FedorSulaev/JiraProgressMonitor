using System.Web.Mvc;
using ProgressMonitor.Constants;

namespace ProgressMonitor.Controllers
{
	[Authorize(Roles = UserRoles.AdminRole)]
    public class UserManagementController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}