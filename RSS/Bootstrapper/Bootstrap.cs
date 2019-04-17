using Autofac;
using RSS.Services;

namespace RSS.Bootstrapper
{
	public class Bootstrap
	{
		public static IContainer Container { get; set; }

		static Bootstrap()
		{
			var builder = new ContainerBuilder();

			/*
			var onPlatformHapticService = XF.DependencyService.Get<IHapticService>();
			var onPlatformMapService = XF.DependencyService.Get<IMapService>();
			builder.RegisterInstance(onPlatformMapService).AsImplementedInterfaces();
			*/

			builder.RegisterType<UKNewsService>().AsImplementedInterfaces();
			builder.RegisterType<HttpClientService>().AsImplementedInterfaces();

			Container = builder.Build();
		}
	}
}