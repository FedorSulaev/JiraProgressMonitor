using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProgressMonitor.Constants;
using ProgressMonitor.Models;

namespace ProgressMonitor.Migrations
{
	using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<ProgressMonitor.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override async void Seed(ProgressMonitor.Models.ApplicationDbContext context)
        {
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//
			RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
			RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);
			if (!context.Roles.Any(r => r.Name == UserRoles.AdminRole))
	        {
				IdentityRole role = new IdentityRole {Name = UserRoles.AdminRole};
		        await roleManager.CreateAsync(role);
	        }
	        string adminRoleId = roleManager.Roles.First(r => r.Name == UserRoles.AdminRole).Id;
	        if (!context.Users.Any(u => u.Roles.Any(r => r.RoleId == adminRoleId)))
	        {
		        UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
				UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);
		        ApplicationUser adminUser = new ApplicationUser {UserName = "admin"};
		        await userManager.CreateAsync(adminUser, "0000");
		        await userManager.AddToRoleAsync(adminUser.Id, UserRoles.AdminRole);
	        }
        }
    }
}
