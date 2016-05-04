using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProgressMonitor.Constants;
using ProgressMonitor.Models;
using ProgressMonitor.Models.DbModels;
using ProgressMonitor.Models.JsonSerialization;
using ProgressMonitor.Services;

namespace ProgressMonitor.Controllers
{
	[Authorize(Roles = UserRoles.AdminRole)]
    public class UserManagementController : Controller
    {
		private readonly IJiraAPIService _jiraAPIService;
		private readonly ApplicationDbContext _context;

		public UserManagementController(IJiraAPIService jiraAPIService)
		{
			_jiraAPIService = jiraAPIService;
			_context = new ApplicationDbContext();
		}

		public ActionResult Index()
		{
			var model = _context.Users.Select(u => new UserViewModel
			{
				Username = u.UserName,
				Id = u.Id
			}).ToList();
            return View(model);
        }

		public ActionResult UserSettings(string userId)
		{
			IReadOnlyDictionary<JiraProject, bool> projectAccess =  _jiraAPIService.GetProjectAccessForUser(
				userId);
			var model = new UserSettingsViewModel
			{
				Username = _context.Users.First(u => u.Id == userId).UserName,
				Id = userId,
				ProjectAccess = projectAccess
			};
			return View(model);
		}

		[HttpPost]
		public ActionResult UserSettings(UserSettingsViewModel model)
		{
			return View();
		}
    }
}