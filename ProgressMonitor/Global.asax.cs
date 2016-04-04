using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using ProgressMonitor.Modules;

namespace ProgressMonitor
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
	        DependencyResolver.SetResolver(CreateAutofacDependencyResolver());
        }

	    private IDependencyResolver CreateAutofacDependencyResolver()
	    {
		    ContainerBuilder builder = new ContainerBuilder();
		    RegisterDependencies(builder);
		    IContainer container = builder.Build();
			return new AutofacDependencyResolver(container);
	    }

	    private void RegisterDependencies(ContainerBuilder builder)
	    {
			builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
		    builder.RegisterModule(new ServiceModule());
	    }
    }
}
