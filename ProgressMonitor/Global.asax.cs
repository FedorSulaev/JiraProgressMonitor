using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;

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
		    builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
		    IContainer container = builder.Build();
			return new AutofacDependencyResolver(container);
	    }
    }
}
