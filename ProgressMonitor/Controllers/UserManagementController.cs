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
			if (!_context.Users.Any(u => u.Id == userId))
			{
				return null;
			}
			IReadOnlyDictionary<JiraProject, bool> projectAccess = 
				_jiraAPIService.GetProjectAccessForUser(userId);
			var model = new UserSettingsViewModel
			{
				Username = _context.Users.First(u => u.Id == userId).UserName,
				Id = userId,
				ProjectAccess = projectAccess.Select(pa => new ProjectAccessViewModel
				{
					CanAccess = pa.Value,
					ProjectId = pa.Key.Id,
					ProjectName = pa.Key.Name
				}).ToList()
			};
			return View(model);
		}

		[HttpPost]
		public ActionResult UserSettings(UserSettingsViewModel model)
		{
			ApplicationUser user = _context.Users.First(u => u.Id == model.Id);
			List<Project> temp = new List<Project>();
			foreach (var project in user.AccessibleProjects.Where(p =>
				model.ProjectAccess.All(a => a.ProjectId != p.JiraId) 
				|| model.ProjectAccess.First(a => a.ProjectId == p.JiraId).CanAccess == false))
			{
				temp.Add(project);
			}
			foreach (Project project in temp)
			{
				user.AccessibleProjects.Remove(project);
			}
			temp.Clear();
			foreach (var projectAccess in model.ProjectAccess.Where(a => a.CanAccess 
				&& user.AccessibleProjects.All(p => p.JiraId != a.ProjectId)))
			{
				temp.Add(new Project {JiraId = projectAccess.ProjectId});
			}
			foreach (Project project in temp)
			{
				user.AccessibleProjects.Add(project);
			}
			_context.SaveChanges();
			return UserSettings(model.Id);
		}
    }
}