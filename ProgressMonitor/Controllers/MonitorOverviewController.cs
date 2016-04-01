using System.Web.Mvc;

namespace ProgressMonitor.Controllers
{
	[Authorize]
    public class MonitorOverviewController : Controller
    {
        // GET: MonitorOverview
        public ActionResult Index()
        {
            return View();
        }
    }
}