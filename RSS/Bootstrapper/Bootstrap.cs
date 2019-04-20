using Autofac;
using RSS.Commands;
using RSS.Services;
using XF = Xamarin.Forms;

namespace RSS.Bootstrapper
{
	public class Bootstrap
	{
		public static IContainer Container { get; set; }

		static Bootstrap()
		{
			var builder = new ContainerBuilder();

			builder.RegisterType<HttpClientService>().AsImplementedInterfaces();
			builder.RegisterType<NewsService>().AsImplementedInterfaces();

			var displayToastCommand = XF.DependencyService.Get<IDisplayToastCommand>();
			builder.RegisterInstance(displayToastCommand).AsImplementedInterfaces();

			Container = builder.Build();
		}
	}
}