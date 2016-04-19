using System.Web.Mvc;

namespace ProgressMonitor.Controllers
{
    public class VersionController : Controller
    {
        public ActionResult Index(long id)
        {
            return View();
        }
    }
}