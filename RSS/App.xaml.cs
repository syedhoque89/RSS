using System;
using Akavache;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RSS
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			BlobCache.ApplicationName = "RSS";
			MainPage = new MainPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
