using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace ProgressMonitor.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(Assembly.Load("ProgressMonitor"))
				.Where(t => t.Name.EndsWith("Service"))
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();
		}
	}
}