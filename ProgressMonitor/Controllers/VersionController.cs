﻿using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProgressMonitor.Constants;
using ProgressMonitor.Models.DbModels;
using ProgressMonitor.Services;

namespace ProgressMonitor.Controllers
{
	[Authorize]
    public class VersionController : Controller
    {
	    private readonly IJiraAPIService _jiraAPIService;
		private readonly ProgressMonitorDbContext _context;

	    public VersionController(IJiraAPIService jiraAPIService)
	    {
		    _jiraAPIService = jiraAPIService;
			_context = new ProgressMonitorDbContext();
	    }

	    public ActionResult Index(long versionId, long projectId)
	    {
		    string userId = User.Identity.GetUserId();
			if (!User.IsInRole(UserRoles.AdminRole) && _context.Users.First(u => u.Id == userId)
				.AccessibleProjects.All(p => p.JiraId != projectId))
			{
				return null;
			}
			var model = _jiraAPIService.GetIssuesByVersion(versionId);
            return View(model);
        }
    }
}